using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class KeyAtSite : DomainEntityId
{
    public Guid Id { get; set; }
    
    
    public Guid KeyId { get; set; }
    public Key? Key { get; set; }
    
    public Guid? SiteId { get; set; }
    public Site? Site { get; set; } 
    
}