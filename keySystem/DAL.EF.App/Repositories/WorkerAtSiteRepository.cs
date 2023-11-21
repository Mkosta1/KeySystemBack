using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class WorkerAtSiteRepository :
    EFBaseRepository<WorkerAtSite, ApplicationDbContext>, IWorkerAtSiteRepository
{
    public WorkerAtSiteRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<WorkerAtSite>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(o => o.Site)
            .Include(o => o.Worker)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<WorkerAtSite>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Site)
            .ToListAsync();
    }

    public override async Task<WorkerAtSite?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .OrderBy(t => t.Id)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<WorkerAtSite?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<WorkerAtSite?> RemoveAsync(Guid id, Guid userId)
    {
        var rally = await FindAsync(id, userId);
        return rally == null ? null : Remove(rally);
    }
}