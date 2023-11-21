using DAL.Contracts.App;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private IKeyRepository? _keyRepository;

    public IKeyRepository KeyRepository =>
        _keyRepository ??= new KeyRepository(UowDbContext);
    
    private ISiteRepository? _siteRepository;

    public ISiteRepository SiteRepository =>
        _siteRepository ??= new SiteRepository(UowDbContext);
    
    private IWorkerRepository? _workerRepository;

    public IWorkerRepository WorkerRepository =>
        _workerRepository ??= new WorkerRepository(UowDbContext);
    
    private IWorkerAtSiteRepository? _workerAtSiteRepository;

    public IWorkerAtSiteRepository WorkerAtSiteRepository =>
        _workerAtSiteRepository ??= new WorkerAtSiteRepository(UowDbContext);
    
    private IKeyAtSiteRepository? _keyAtSiteRepository;

    public IKeyAtSiteRepository KeyAtSiteRepository =>
        _keyAtSiteRepository ??= new KeyAtSiteRepository(UowDbContext);
    
}