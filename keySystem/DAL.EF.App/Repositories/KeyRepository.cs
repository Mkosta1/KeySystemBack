using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class KeyRepository :
    EFBaseRepository<Key, ApplicationDbContext>, IKeyRepository
{
    public KeyRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Key>> AllAsync()
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<Key>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public override async Task<Key?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .OrderBy(t => t.Id)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<Key?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Key?> RemoveAsync(Guid id, Guid userId)
    {
        var job = await FindAsync(id, userId);
        return job == null ? null : Remove(job);
    }
}