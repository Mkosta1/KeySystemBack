using DAL.Contracts.Base;
using Domain;
using Domain.App;

namespace DAL.Contracts.App;

public interface IWorkerRepository : IBaseRepository<Worker>
{
    Task<IEnumerable<Worker>> AllAsync(Guid userId);
    Task<Worker?> FindAsync(Guid id, Guid userId);
    Task<Worker?> RemoveAsync(Guid id, Guid userId);
}