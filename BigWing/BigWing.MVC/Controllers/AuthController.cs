using BigWing.Core.Entities;
using BigWing.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BigWing.MVC.Controllers;
public class AuthController(
    AppDbContext _context,
    SignInManager<User> _signInManager,
    UserManager<User> _userManager,
    IWebHostEnvironment _env
    ) : Controller
{
    bool isAuthenticated => User.Identity?.IsAuthenticated ?? false;
    public IActionResult Register()
    {
        if (isAuthenticated)
        {

        }
        return View();
    }
    /*[HttpPost]*/
    /*public async Task<IActionResult> Register()
    {
        if (isAuthenticated)
        {

        }
    }*/
}
