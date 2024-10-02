using TaskResolution.Application.Core.DomainModels;

namespace TaskResolution.Application.Core.Contracts
{
    public interface ISubjectsRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
    }
}