using BigWing.BL.Extension;
using BigWing.BL.VM.User;
using BigWing.Core.Entities;
using BigWing.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigWing.MVC.Controllers;
public class AuthController : Controller
{

    private readonly AppDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _env;

    public AuthController(
        AppDbContext context,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        IWebHostEnvironment env)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _env = env;
    }
    bool isAuthenticated => User.Identity?.IsAuthenticated ?? false;
    public async Task<IActionResult> Register()
    {
        if (isAuthenticated)
            return RedirectToAction("Index", "Home");
        ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserCreateVM vm)
    {
        if (isAuthenticated)
            return RedirectToAction("Index", "Home");
        if (!ModelState.IsValid) 
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();

        User user = new User
        {
            Name = vm.Name,
            Surname = vm.Surname,
            Email = vm.Email,
            Comment = vm.Comment,
            DepartmentId = vm.DepartmentId,
            ProfileImageUrl = await vm.ProfileImage!.UploadAsync(_env.WebRootPath, "imgs", "user"),
            UserName = vm.Username
        };
        if(vm.Password != vm.RePassword){
            ModelState.AddModelError("password", "password-ler uygun deyil!"); 
        }
        var result =await _userManager.CreateAsync(user, vm.Password);   
        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }
        return RedirectToAction(nameof(Login));

    }
    public async Task<IActionResult> Login()
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        User? user = null;
        if (vm.UsernameOrEmail.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
        }
        else
        {
            user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
        }
        if (user == null)
        {
            ModelState.AddModelError("", "Username or Password is wrong.");
        }
        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
        if (!result.Succeeded)
        {
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Username or password is wrong!");
            }
            return View();
        }
        return RedirectToAction("Index", "Admin");
    }
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
