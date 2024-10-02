using Microsoft.AspNetCore.Mvc;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Controllers
{
    /// <summary>
    /// Manages student-related operations including retrieving, creating, updating, and deleting student records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        #region Properties

        /// <summary>
        /// Service for managing student operations.
        /// </summary>
        private readonly IStudentsService studentsService;

        /// <summary>
        /// Service for managing subject operations.
        /// </summary>
        private readonly ISubjectsService subjectsService;

        /// <summary>
        /// Logger for tracking application events and errors.
        /// </summary>
        private readonly Loggers.ILogger logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsController"/> class.
        /// </summary>
        /// <param name="studentsService">The service for handling student operations.</param>
        /// <param name="subjectsService">The service for handling subject operations.</param>
        /// <param name="logger">The logger instance for logging operations.</param>
        public StudentsController(IStudentsService studentsService, ISubjectsService subjectsService, Loggers.ILogger logger)
        {
            this.studentsService = studentsService;
            this.subjectsService = subjectsService;
            this.logger = logger;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves a student by their unique number.
        /// </summary>
        /// <param name="number">The unique number of the student.</param>
        /// <returns>An action result containing the student data.</returns>
        [HttpGet("{number}")]
        public async Task<IActionResult> GetStudent(string number)
        {
            try
            {
                var student = await studentsService.GetByIdAsync(number);
                if (student == null)
                {
                    logger.LogWarning($"Student with Number {number} not found.");
                    return NotFound();
                }
                logger.LogInfo($"Successfully retrieved student with Number {number}.");
                return Ok(student);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to retrieve student with Number {number}.");
                return BadRequest("An error occurred while retrieving the student.");
            }
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>An action result containing a list of all students.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await studentsService.GetAllAsync();
                logger.LogInfo("Successfully retrieved all students.");
                return Ok(students);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve all students.");
                return BadRequest("An error occurred while retrieving the students.");
            }
        }

        /// <summary>
        /// Creates a new student record.
        /// </summary>
        /// <param name="studentDto">The data transfer object containing student information.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentDto studentDto)
        {
            try
            {
                if (studentDto == null || !ModelState.IsValid)
                {
                    logger.LogWarning("Invalid student data provided.");
                    return BadRequest(ModelState);
                }

                await studentsService.AddAsync(studentDto);
                logger.LogInfo($"Successfully created student with number {studentDto.Number}.");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create student.");
                return BadRequest("An error occurred while creating the student.");
            }
        }

        /// <summary>
        /// Updates an existing student record.
        /// </summary>
        /// <param name="number">The unique number of the student to be updated.</param>
        /// <param name="studentDto">The data transfer object containing updated student information.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpPut("{number}")]
        public async Task<IActionResult> UpdateStudent(string number, [FromBody] AddStudentDto studentDto)
        {
            try
            {
                if (studentDto == null || !ModelState.IsValid)
                {
                    logger.LogWarning("Invalid student data provided.");
                    return BadRequest(ModelState);
                }

                await studentsService.UpdateAsync(studentDto);
                logger.LogInfo($"Successfully updated student with Number {number}.");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update student with Number {number}.");
                return BadRequest("An error occurred while updating the student.");
            }
        }

        /// <summary>
        /// Deletes a student record by their unique number.
        /// </summary>
        /// <param name="number">The unique number of the student to be deleted.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpDelete("{number}")]
        public async Task<IActionResult> DeleteStudent(string number)
        {
            try
            {
                await studentsService.DeleteAsync(number);
                logger.LogInfo($"Successfully deleted student with Number {number}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to delete student with Number {number}.");
                return BadRequest("An error occurred while deleting the student.");
            }
        }

        /// <summary>
        /// Retrieves all subjects.
        /// </summary>
        /// <returns>An action result containing a list of all subjects.</returns>
        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {
                var subjects = await subjectsService.GetAllAsync();
                logger.LogInfo("Successfully retrieved all subjects.");
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve subjects.");
                return BadRequest("An error occurred while retrieving subjects.");
            }
        }

        #endregion
    }
}