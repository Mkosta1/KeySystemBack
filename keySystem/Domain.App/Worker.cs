using Domain.Base;

namespace Domain.App;

public class Worker : DomainEntityId
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    
    public string Company { get; set; } = default!;
    
    public int Phone { get; set; }
    
    public ICollection<WorkerAtSite>? WorkerAtSite { get; set; }
}