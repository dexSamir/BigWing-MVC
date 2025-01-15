
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BigWing.MVC.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); 
    }
}
