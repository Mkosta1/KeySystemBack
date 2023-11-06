using System.ComponentModel.DataAnnotations;

namespace DAL.DTO;

public class Worker
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    
    public string Company { get; set; } = default!;
    
    public int Phone { get; set; }

    public ICollection<WorkerAtSite>? WorkerAtSite { get; set; }
}