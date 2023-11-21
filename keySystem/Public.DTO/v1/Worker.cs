
namespace Public.DTO.v1;

public class Worker
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    
    public string Company { get; set; } = default!;
    
    public int Phone { get; set; } = default!;
    
}