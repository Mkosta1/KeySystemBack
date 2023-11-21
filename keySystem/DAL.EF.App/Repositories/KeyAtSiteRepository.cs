using System.Text.RegularExpressions;
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class KeyAtSiteRepository :
    EFBaseRepository<KeyAtSite, ApplicationDbContext>, IKeyAtSiteRepository
{
    public KeyAtSiteRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<KeyAtSite>> AllAsync()
    {
        var data = await RepositoryDbSet
            .Include(o => o.Key)
            .Include(o => o.Site)
            .ThenInclude(o => o!.WorkerAtSite)
            .Where(kas => kas.Site!.WorkerAtSite!.All(was => was.Until != null))
            .ToListAsync();

        var groupedData = data
            .GroupBy(kas => kas.Site)
            .Select(group =>
            {
                var firstKeyAtSite = group.First();
                firstKeyAtSite.Key!.KeyNumber = string.Join(", ", group.Select(k => k.Key!.KeyNumber));
                return firstKeyAtSite;
            })
            .ToList();
        return groupedData;
    }
    

    public virtual async Task<IEnumerable<KeyAtSite>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Site)
            .ToListAsync();
    }

    public override async Task<KeyAtSite?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .OrderBy(t => t.Id)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    

    public async Task<IEnumerable<KeyAtSite>> FindSiteAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(m => m.SiteId == id)
            .ToListAsync();
    }
    
    public virtual async Task<KeyAtSite?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<KeyAtSite?> RemoveAsync(Guid id, Guid userId)
    {
        var rally = await FindAsync(id, userId);
        return rally == null ? null : Remove(rally);
    }
}