using System.Runtime.InteropServices.JavaScript;
using Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("bc7458ac-cbb0-4ecd-be79-d5abf19f8c77");

    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string pwd) userData = (adminId, "admin@app.com", "Foo.bar.1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new AppUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                FirstName = "Admin",
                LastName = "App",
                EmailConfirmed = true,
            };
            var result = userManager.CreateAsync(user, userData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result.ToString()}");
            }
        }
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataKey(context);
        SeedAppDataSite(context);
        SeedAppDataWorker(context);
        SeedAppDataWorkerAtSite(context);
    
        context.SaveChanges();
    }

    private static void SeedAppDataKey(ApplicationDbContext context)
    {
        if (context.Key.Any()) return;
    
        context.Key.Add(new Key()
            {
                Id = adminId,
                Name = "Lombaku",
                Copies = 2,
                NeededCopies = 2,
                KeyNumber = "A60",
                
            }
        );
    }
    
    
    private static void SeedAppDataSite(ApplicationDbContext context)
    {
        if (context.Site.Any()) return;
    
        context.Site.Add(new Site()
            {
                Id = Guid.Parse("e0069111-1fe7-435b-837e-4ea2cb4e9c63"),
                SiteId = "234IDA",
                Name = "test",
                Address = "ojiqret",
                Region = "Harjumaa",
                Owner = "Tele2",
                KeyId = adminId

            }
        );
    }
    
    private static void SeedAppDataWorker(ApplicationDbContext context)
    {
        if (context.Worker.Any()) return;
    
        context.Worker.Add(new Worker()
            {
                Id = Guid.Parse("911de4a6-1852-4ac9-aaa8-792223bd6656"),
                Name = "Madis Kuremäelt",
                Company = "T2",
                Phone = 559492390
            }
        );
    }
    
    private static void SeedAppDataWorkerAtSite(ApplicationDbContext context)
    {
        if (context.WorkerAtSite.Any()) return;
    
        context.WorkerAtSite.Add(new WorkerAtSite()
            {
                
                When = DateTime.Today,
                Until = null,
                SiteId = Guid.Parse("e0069111-1fe7-435b-837e-4ea2cb4e9c63"),
                WorkerId = Guid.Parse("911de4a6-1852-4ac9-aaa8-792223bd6656"),
                AppUserId = adminId

            }
        );
    }
}