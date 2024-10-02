using AutoMapper;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.DomainModels;
using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Application.Core.Services
{
    /// <summary>
    /// Service for managing student operations, including retrieval, creation, updating, and deletion of student records.
    /// </summary>
    public class StudentsService : IStudentsService
    {
        #region Properties

        /// <summary>
        /// Repository for accessing student data.
        /// </summary>
        private readonly IStudentsRepository studentsRepository;

        /// <summary>
        /// Mapper for converting between domain models and data transfer objects.
        /// </summary>
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsService"/> class.
        /// </summary>
        /// <param name="studentsRepository">The repository for managing student data.</param>
        /// <param name="mapper">The mapper for converting between domain models and DTOs.</param>
        public StudentsService(IStudentsRepository studentsRepository, IMapper mapper)
        {
            this.studentsRepository = studentsRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves all students from the repository.
        /// </summary>
        /// <returns>A collection of student data transfer objects.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while retrieving students.</exception>
        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            try
            {
                var students = await studentsRepository.GetAllAsync();
                return mapper.Map<IEnumerable<StudentDto>>(students);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving students.", ex);
            }
        }

        /// <summary>
        /// Retrieves a student by their unique number.
        /// </summary>
        /// <param name="number">The unique number of the student.</param>
        /// <returns>A student data transfer object.</returns>
        /// <exception cref="Exception">Thrown if the student is not found or an error occurs.</exception>
        public async Task<StudentDto> GetByIdAsync(string number)
        {
            try
            {
                var student = await studentsRepository.GetByNumberAsync(number);
                if (student == null)
                {
                    throw new Exception($"Student with Number {number} not found.");
                }
                return mapper.Map<StudentDto>(student);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the student with Number {number}.", ex);
            }
        }

        /// <summary>
        /// Adds a new student to the repository.
        /// </summary>
        /// <param name="studentDto">The data transfer object containing student information.</param>
        /// <exception cref="Exception">Thrown if an error occurs while creating the student.</exception>
        public async Task AddAsync(AddStudentDto studentDto)
        {
            try
            {
                var student = mapper.Map<Student>(studentDto);
                student.ID = Guid.NewGuid();
                await studentsRepository.AddAsync(student);
                await studentsRepository.AttachSubjectAsync(student.ID, studentDto.Subjects);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the student.", ex);
            }
        }

        /// <summary>
        /// Updates an existing student's information.
        /// </summary>
        /// <param name="studentDto">The data transfer object containing updated student information.</param>
        /// <exception cref="Exception">Thrown if the student is not found or an error occurs while updating.</exception>
        public async Task UpdateAsync(AddStudentDto studentDto)
        {
            try
            {
                var student = await studentsRepository.GetByNumberAsync(studentDto.Number);
                if (student == null)
                {
                    throw new Exception($"Student with Number {studentDto.Number} not found.");
                }

                // Update student properties from the DTO
                student.FirstName = studentDto.FirstName;
                student.MidName = studentDto.MidName;
                student.LastName = studentDto.LastName;
                student.BirthDate = studentDto.BirthDate;

                // Update student in repository
                await studentsRepository.UpdateAsync(student);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the student with Number {studentDto.Number}.", ex);
            }
        }

        /// <summary>
        /// Deletes a student by their unique number.
        /// </summary>
        /// <param name="number">The unique number of the student to be deleted.</param>
        /// <exception cref="Exception">Thrown if the student is not found or an error occurs while deleting.</exception>
        public async Task DeleteAsync(string number)
        {
            try
            {
                var student = await studentsRepository.GetByNumberAsync(number);
                if (student == null)
                {
                    throw new Exception($"Student with Number {number} not found.");
                }
                await studentsRepository.DeleteAsync(number);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the student with Number {number}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all subjects associated with a specific student.
        /// </summary>
        /// <param name="studentNumber">The unique number of the student.</param>
        /// <returns>A collection of subject data transfer objects associated with the student.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while retrieving subjects.</exception>
        public async Task<IEnumerable<SubjectDto>> GetSubjectsByStudentIdAsync(string studentNumber)
        {
            try
            {
                var subjects = await studentsRepository.GetSubjectsByStudentNumberAsync(studentNumber);
                return mapper.Map<IEnumerable<SubjectDto>>(subjects);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving subjects for student with Number {studentNumber}.", ex);
            }
        }

        /// <summary>
        /// Adds subjects to a specific student.
        /// </summary>
        /// <param name="studentNumber">The unique number of the student.</param>
        /// <param name="subjectCodes">The codes of the subjects to be added.</param>
        /// <exception cref="Exception">Thrown if an error occurs while adding subjects.</exception>
        public async Task AddSubjectsToStudentAsync(string studentNumber, IEnumerable<string> subjectCodes)
        {
            try
            {
                await studentsRepository.AddSubjectsToStudentAsync(studentNumber, subjectCodes);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding subjects to student with Number {studentNumber}.", ex);
            }
        }

        /// <summary>
        /// Removes subjects from a specific student.
        /// </summary>
        /// <param name="studentNumber">The unique number of the student.</param>
        /// <param name="subjectCodes">The codes of the subjects to be removed.</param>
        /// <exception cref="Exception">Thrown if an error occurs while removing subjects.</exception>
        public async Task RemoveSubjectsFromStudentAsync(string studentNumber, IEnumerable<string> subjectCodes)
        {
            try
            {
                await studentsRepository.RemoveSubjectsFromStudentAsync(studentNumber, subjectCodes);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing subjects from student with Number {studentNumber}.", ex);
            }
        }

        #endregion
    }
}