using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers;

public class GameNightController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Organized()
    {
        return View(); 
    }

    public IActionResult Participating()
    {
        return View(); 
    }
}