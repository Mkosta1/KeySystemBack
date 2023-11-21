using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IKeyAtSiteRepository : IBaseRepository<KeyAtSite>
{
    Task<IEnumerable<KeyAtSite>> AllAsync(Guid userId);
    Task<KeyAtSite?> FindAsync(Guid id, Guid userId);
    Task<KeyAtSite?> RemoveAsync(Guid id, Guid userId);
}