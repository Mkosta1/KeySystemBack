using System.ComponentModel.DataAnnotations;
using Site = Public.DTO.v1.Site;

namespace Public.DTO.v1;

public class Key
{
    public Guid Id { get; set; }
    [MinLength(1)]
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    public int Copies { get; set; } = default!;

    public int NeededCopies { get; set; } = default!;
    
    public string KeyNumber { get; set; } = default!;
    
}