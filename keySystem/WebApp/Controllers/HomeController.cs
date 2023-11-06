using System.Diagnostics;
using DAL.EF.App;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public string TestAlpha(string bar)
    {
        return "string " + bar;
    }
    public string Test(int bar)
    {
        return "int " + bar;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var vm = new HomePrivacyVM();
        
        if (User.Identity == null) return View(vm);
        
        vm.AppUser = await _context.Users
            .Include(u => u.AppRefreshTokens)
            .FirstOrDefaultAsync(u => u.Id== User.GetUserId());
        vm.AppUserClaims = await _context.UserClaims.Where(u => u.UserId == User.GetUserId()).ToListAsync();

        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}