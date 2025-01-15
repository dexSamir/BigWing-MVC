
using BigWing.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BigWing.MVC.Controllers;
public class HomeController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var clients = await _context.Users.Include(x => x.Department).ToListAsync();

        return View(clients);

    }
}
