using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Application.Core.Contracts
{
    public interface IStudentsService
    {
        // Get all students
        Task<IEnumerable<StudentDto>> GetAllAsync();

        // Get a student by ID
        Task<StudentDto> GetByIdAsync(string studentNumber);

        // Add a new student
        Task AddAsync(AddStudentDto studentDto);

        // Update an existing student
        Task UpdateAsync(AddStudentDto studentDto);

        // Delete a student by ID
        Task DeleteAsync(string studentNumber);

        // Get subjects for a specific student
        Task<IEnumerable<SubjectDto>> GetSubjectsByStudentIdAsync(string studentNumber);

        // Add subjects to a student
        Task AddSubjectsToStudentAsync(string studentNumber, IEnumerable<string> subjectCodes);

        // Remove subjects from a student
        Task RemoveSubjectsFromStudentAsync(string studentNumber, IEnumerable<string> subjectCodes);
    }
}