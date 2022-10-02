using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

public class GameNightController : Controller
{
    private readonly IGameNightRepository _repository;

    public GameNightController(IGameNightRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var gameNights = _repository.GetAllGameNights();

        return View(gameNights);
    }

    public IActionResult Organized()
    {
        return View();
    }

    public IActionResult Participating()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Organize()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Organize(GameNightViewModel gameNight)
    {
        var convertedGameNight = new GameNight
        {
            Address = new Address
                { Street = gameNight.Street, City = gameNight.City, HouseNumber = gameNight.HouseNumber },
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = new List<Game>(), Players = new List<User>(),
            DateTime = gameNight.DateTime, IsPotluck = gameNight.IsPotluck, MaxPlayers = gameNight.MaxPlayers,
            IsOnlyForAdults = gameNight.IsOnlyForAdults, Organizer = new User{ Email = "martijnschermers2@gmail.com", Gender = Gender.Male, Name = "Martijn", BirthDate = DateTime.Now, Allergies = new List<Allergy>()}
        };
        
        _repository.AddGameNight(convertedGameNight);

        if (ModelState.IsValid) {
            //Create gamenight
        }

        return View();
    }
}