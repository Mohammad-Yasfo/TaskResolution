using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.DomainModels;

namespace TaskResolution.Repositories.Core.Repositories
{
    public class SubjectsRepository : ISubjectsRepository
    {
        #region Properties
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public SubjectsRepository(StudentsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            // Fetch all subjects and project them into SubjectDto
            var subjectEntities = await context.Subjects
                .ToListAsync();

            return mapper.Map<IEnumerable<Subject>>(subjectEntities);
        }
        #endregion
    }
}
