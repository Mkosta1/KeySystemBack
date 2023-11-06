using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
   // list your repositories here
   IKeyRepository KeyRepository { get; }
   ISiteRepository SiteRepository { get; }
   IWorkerAtSiteRepository WorkerAtSiteRepository { get; }
   IWorkerRepository WorkerRepository { get; }
}