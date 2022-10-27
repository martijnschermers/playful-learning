using System.Diagnostics;
using Core.DomainServices.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

public class HomeController : Controller
{
    private readonly IGameNightRepository _repository;
    
    public HomeController(IGameNightRepository repository)
    {
        _repository = repository;
    }
    
    public IActionResult Index()
    {
        var gameNights = _repository.GetPopularGameNights(); 
        
        return View(gameNights);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}