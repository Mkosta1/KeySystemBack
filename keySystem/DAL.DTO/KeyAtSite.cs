using System.ComponentModel.DataAnnotations;

namespace DAL.DTO;

public class KeyAtSite
{
    public Guid Id { get; set; }
    
    
    public Guid KeyId { get; set; }
    public Key? Key { get; set; }
    
    public Guid SiteId { get; set; }
    public Site? Site { get; set; } 
}