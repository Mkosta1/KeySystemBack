
namespace Public.DTO.v1;

public class WorkerAtSite
{
    public Guid Id { get; set; }

    public DateTime? When { get; set; } = default!;
    public DateTime? Until { get; set; } = default!;

    public Guid SiteId { get; set; }

    public Guid WorkerId { get; set; }

    public Guid AppUserId { get; set; }
}