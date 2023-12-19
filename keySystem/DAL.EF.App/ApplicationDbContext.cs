using Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Key> Key { get; set; } = default!;
    public DbSet<Site> Site { get; set; } = default!;
    public DbSet<Worker> Worker { get; set; } = default!;
    public DbSet<WorkerAtSite> WorkerAtSite { get; set; } = default!;
    
    public DbSet<KeyAtSite> KeyAtSite { get; set; } = default!;
    
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // let the initial stuff run
        base.OnModelCreating(builder);
    
        // set value null to what foreign key it has
        foreach (var foreignKey in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.SetNull;
        }
        
    }
}