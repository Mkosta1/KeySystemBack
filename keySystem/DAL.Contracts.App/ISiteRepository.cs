using DAL.Contracts.Base;
using Domain;
using Domain.App;

namespace DAL.Contracts.App;

public interface ISiteRepository : IBaseRepository<Site>
{
    Task<IEnumerable<Site>> AllAsync(Guid userId);
    Task<Site?> FindAsync(Guid id, Guid userId);
    Task<Site?> RemoveAsync(Guid id, Guid userId);
}