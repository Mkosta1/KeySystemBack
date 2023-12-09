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
            .ToListAsync();

        //This code below checks if key doesnt have 0 copies
        //Also checks if site has more than one key and when one of the key has Count 0, then the site doesnt appear in the all request at all
        var groupedData = data
            .GroupBy(kas => kas.Site)
            .Where(group =>
                    group.All(kas => kas.Key!.Copies > 0) &&  // Check if all keys have more than 0 copies
                    group.All(kas => group.Count(g => g.Key!.Id == kas.Key!.Id && g.SiteId == kas.Site!.Id) == 1)  // Check if each key is assigned at most once
            )
            .Select(group =>
            {
                //If site has multiple keys, then groups them and adds ", " between them
                var firstKeyAtSite = group.First();
                firstKeyAtSite.Key!.KeyNumber = string.Join(", ", group.Select(k => k.Key!.KeyNumber));
                return firstKeyAtSite;
            })
            .ToList();

        return groupedData;
    }
    
    public override async Task<IEnumerable<KeyAtSite>> AllNoCheckAsync()
    {
        var data = await RepositoryDbSet
            .Include(o => o.Key)
            .Include(o => o.Site)
            .ThenInclude(o => o!.WorkerAtSite)
            .ToListAsync();

        //This code below checks if key doesnt have 0 copies
        //Also checks if site has more than one key and when one of the key has Count 0, then the site doesnt appear in the all request at all
        var groupedData = data
            .GroupBy(kas => kas.Site)
            .Select(group =>
            {
                //If site has multiple keys, then groups them and adds ", " between them
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
            .Include(m => m.Site)
            .FirstOrDefaultAsync(m => m.SiteId == id);
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