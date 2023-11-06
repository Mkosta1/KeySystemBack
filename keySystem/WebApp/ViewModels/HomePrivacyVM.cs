using System.Collections.ObjectModel;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class HomePrivacyVM
{
    public AppUser? AppUser { get; set; }
    public ICollection<IdentityUserClaim<Guid>>? AppUserClaims { get; set; }
}