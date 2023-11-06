

using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IKeyRepository : IBaseRepository<Key>
{
    Task<IEnumerable<Key>> AllAsync(Guid userId);
    Task<Key?> FindAsync(Guid id, Guid userId);
    Task<Key?> RemoveAsync(Guid id, Guid userId);
    

}