using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Site : DomainEntityId
{
    public Guid Id { get; set; }
    [MinLength(1)]
    [MaxLength(64)]
    public string SiteId { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public string Address { get; set; } = default!;
    
    public string Region { get; set; } = default!;
    
    public string Owner { get; set; } = default!;
    
    public ICollection<KeyAtSite>? KeyAtSite { get; set; }
    
    public ICollection<WorkerAtSite>? WorkerAtSite { get; set; }

}