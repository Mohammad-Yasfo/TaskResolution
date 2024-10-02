using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Application.Core.Contracts
{
    public interface ISubjectsService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
    }
}