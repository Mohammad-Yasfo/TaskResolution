using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.DomainModels;
using TaskResolution.Repositories.Core.Entities;

namespace TaskResolution.Repositories.Core.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        #region Properties
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public StudentsRepository(StudentsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var studentEntities = await context.Students
                .Include(s => s.StudentSubjects)
                .ToListAsync();

            return mapper.Map<IEnumerable<Student>>(studentEntities);
        }

        public async Task<Student> GetByNumberAsync(string number)
        {
            var studentEntity = await context.Students
                .Include(s => s.StudentSubjects)
                .FirstOrDefaultAsync(s => s.Number == number);

            return mapper.Map<Student>(studentEntity);
        }

        public async Task AddAsync(Student student)
        {
            var studentEntity = mapper.Map<StudentEntity>(student);
            await context.Students.AddAsync(studentEntity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            var studentEntity = await context.Students
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Student)
                .FirstOrDefaultAsync(s => s.ID == student.ID);

            var studentEntityToUpdate = mapper.Map<StudentEntity>(student);
            context.Students.Update(studentEntityToUpdate);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string studentNumber)
        {
            var studentEntity = await context.Students
                .Include(s => s.StudentSubjects)
                .FirstOrDefaultAsync(s => s.Number == studentNumber);

            if (studentEntity != null)
            {
                context.Students.Remove(studentEntity);
                context.StudentSubjects.RemoveRange(studentEntity.StudentSubjects);
                await context.SaveChangesAsync();
            }
        }

        public async Task AttachSubjectAsync(Guid studentId, IList<string> subjectCodes)
        {
            var subjectsToAttach = context.Subjects.Where(s => subjectCodes.Contains(s.Code)).ToList();

            var subjectEntities = subjectsToAttach
                .Select(s => new StudentSubjectEntity()
                {
                    StudentId = studentId,
                    SubjectId = s.ID,
                    CreatedDateTime = DateTime.UtcNow,
                });
            await context.StudentSubjects.AddRangeAsync(subjectEntities);
            await context.SaveChangesAsync();
        }

        public async Task DetachSubjectAsync(Guid studentId, IList<string> subjectCodes)
        {
            var subjectsToDetach = context.Subjects.Where(s => subjectCodes.Contains(s.Code)).ToList();

            var subjectEntities = subjectsToDetach
                .Select(s => new StudentSubjectEntity()
                {
                    StudentId = studentId,
                    SubjectId = s.ID,
                    CreatedDateTime = DateTime.UtcNow,
                });
            context.StudentSubjects.RemoveRange(subjectEntities);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentNumberAsync(string studentNumber)
        {
            var student = await GetByNumberAsync(studentNumber);
            return student?.Subjects ?? Enumerable.Empty<Subject>();
        }

        public async Task AddSubjectsToStudentAsync(string studentNumber, IEnumerable<string> subjectCodes)
        {
            var studentEntity = await context.Students
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Student)
                .FirstOrDefaultAsync(s => s.Number == studentNumber);

            if (studentEntity != null)
            {
                var subjects = await context.Subjects.Where(s => subjectCodes.Contains(s.Code)).ToListAsync();

                foreach (var subject in subjects)
                {
                    studentEntity.StudentSubjects.Add(new StudentSubjectEntity
                    {
                        StudentId = studentEntity.ID,
                        SubjectId = subject.ID,
                        CreatedDateTime = DateTime.UtcNow,
                    });
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveSubjectsFromStudentAsync(string studentNumber, IEnumerable<string> subjectCodes)
        {
            var studentEntity = await context.Students
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Student)
                .FirstOrDefaultAsync(s => s.Number == studentNumber);

            if (studentEntity != null)
            {
                var subjectsToRemove = context.Subjects.Where(s => subjectCodes.Contains(s.Code)).ToList();
                foreach (var subject in subjectsToRemove)
                {
                    var subjectEntity = studentEntity.StudentSubjects
                        .First(s => s.StudentId == studentEntity.ID && s.SubjectId == subject.ID);

                    context.StudentSubjects.Remove(subjectEntity);
                }
                await context.SaveChangesAsync();
            }
        }
        #endregion
    }
}