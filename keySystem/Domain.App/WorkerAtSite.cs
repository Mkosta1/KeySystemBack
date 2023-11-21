
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class WorkerAtSite : DomainEntityId
{
    public Guid Id { get; set; }

    public DateTime? When { get; set; } = default!;
    public DateTime? Until { get; set; } = default!;

    public Guid SiteId { get; set; }
    public Domain.App.Site? Site { get; set; }
    
    public Guid? WorkerId { get; set; }
    public Domain.App.Worker? Worker { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
}