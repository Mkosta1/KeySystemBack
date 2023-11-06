using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.EF.App;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // does not actually connect to db
        optionsBuilder.UseNpgsql("Host=localhost:5446;Database=sportschool-exam;Username=postgres;Password=postgres;Include Error Detail=true");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}