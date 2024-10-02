using AutoMapper;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Application.Core.Services
{
    /// <summary>
    /// Provides services for managing subjects.
    /// </summary>
    public class SubjectsService : ISubjectsService
    {
        #region Properties

        /// <summary>
        /// The repository for accessing subject data.
        /// </summary>
        private readonly ISubjectsRepository subjectsRepository;

        /// <summary>
        /// The mapper for converting domain models to data transfer objects.
        /// </summary>
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectsService"/> class.
        /// </summary>
        /// <param name="subjectsRepository">The subject repository.</param>
        /// <param name="mapper">The mapper for converting domain models to DTOs.</param>
        public SubjectsService(ISubjectsRepository subjectsRepository, IMapper mapper)
        {
            this.subjectsRepository = subjectsRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously retrieves all subjects.
        /// </summary>
        /// <returns>A collection of <see cref="SubjectDto"/> representing all subjects.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving subjects.</exception>
        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            try
            {
                var subjects = await subjectsRepository.GetAllAsync();
                return mapper.Map<IEnumerable<SubjectDto>>(subjects);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subjects.", ex);
            }
        }

        #endregion
    }
}