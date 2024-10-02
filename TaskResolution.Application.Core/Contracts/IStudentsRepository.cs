using TaskResolution.Application.Core.DomainModels;

namespace TaskResolution.Application.Core.Contracts
{
    public interface IStudentsRepository
    {
        // Get all students
        Task<IEnumerable<Student>> GetAllAsync();

        // Get a student by ID
        Task<Student> GetByNumberAsync(string number);

        // Add a new student
        Task AddAsync(Student student);

        // Update an existing student
        Task UpdateAsync(Student student);

        // Delete a student by ID
        Task DeleteAsync(string number);

        Task AttachSubjectAsync(Guid studentId, IList<string> subjectCodes);

        Task DetachSubjectAsync(Guid studentId, IList<string> subjectCodes);

        // Get subjects for a specific student
        Task<IEnumerable<Subject>> GetSubjectsByStudentNumberAsync(string studentNumber);

        // Add subjects to a student
        Task AddSubjectsToStudentAsync(string studentNumber, IEnumerable<string> subjectCodes);

        // Remove subjects from a student
        Task RemoveSubjectsFromStudentAsync(string studentNumber, IEnumerable<string> subjectCodes);
    }
}