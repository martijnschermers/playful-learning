using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

[Authorize]
public class GameNightController : Controller
{
    private readonly IGameNightRepository _gameNightRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public GameNightController(IGameNightRepository gameNightRepository, IGameRepository gameRepository, IUserRepository userRepository)
    {
        _gameNightRepository = gameNightRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        var gameNights = _gameNightRepository.GetAllGameNights();

        return View(gameNights);
    }

    public IActionResult Organized()
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity.Name);
        
        var gameNights = _gameNightRepository.GetAllGameNights().Where(g => g.Organizer == user);
        
        return View(gameNights);
    }

    public IActionResult Participating()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Organize()
    {
        //TODO: Fix select menu in Organize.html for games
        // var games = _gameRepository.GetAllGames().Select(g => g.Name);
        // ViewBag.Games = new SelectList(games, "Name", "Name");
        
        return View(new GameNightViewModel());
    }

    [HttpPost]
    public IActionResult Organize(GameNightViewModel gameNightViewModel)
    {
        if (!ModelState.IsValid) {
            return View(); 
        }
        
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity.Name);

        var gameNight = new GameNight
        {
            Address = new Address
                { Street = gameNightViewModel.Street, City = gameNightViewModel.City, HouseNumber = gameNightViewModel.HouseNumber },
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = new List<Game>(), Players = new List<User>(),
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck, MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };
        
        _gameNightRepository.AddGameNight(gameNight);

        return RedirectToAction(nameof(Organized));
    }
}