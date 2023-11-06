using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class SiteRepository :
    EFBaseRepository<Site, ApplicationDbContext>, ISiteRepository
{
    public SiteRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Site>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Key)
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<Site>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public override async Task<Site?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .OrderBy(t => t.Id)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<Site?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Site?> RemoveAsync(Guid id, Guid userId)
    {
        var partAtJob = await FindAsync(id, userId);
        return partAtJob == null ? null : Remove(partAtJob);
    }
}