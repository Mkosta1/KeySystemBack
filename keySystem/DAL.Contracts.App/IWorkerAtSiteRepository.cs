using DAL.Contracts.Base;
using Domain;
using Domain.App;

namespace DAL.Contracts.App;

public interface IWorkerAtSiteRepository : IBaseRepository<WorkerAtSite>
{
    Task<IEnumerable<WorkerAtSite>> AllAsync(Guid userId);
    Task<WorkerAtSite?> FindAsync(Guid id, Guid userId);
    
    Task<WorkerAtSite?> RemoveAsync(Guid id, Guid userId);
}