using System.ComponentModel.DataAnnotations;

namespace DAL.DTO;

public class Key
{
    public Guid Id { get; set; }
    [MinLength(1)]
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    public int Copies { get; set; } = default!;

    public int NeededCopies { get; set; } = default!;
    
    public string KeyNumber { get; set; } = default!;

    
    public ICollection<Site>? Site { get; set; }
}