﻿using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1;

public class Site
{
    public Guid Id { get; set; }
    [MinLength(1)]
    [MaxLength(64)]
    public string SiteId { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public string Address { get; set; } = default!;
    
    public string Region { get; set; } = default!;
    
    public string Owner { get; set; } = default!;
    
    
    

}