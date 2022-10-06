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

    public GameNightController(IGameNightRepository gameNightRepository, IGameRepository gameRepository,
        IUserRepository userRepository)
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
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var gameNights = _gameNightRepository.GetAllGameNights().Where(g => g.Organizer == user);

        return View(gameNights);
    }

    public IActionResult Participating()
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var gameNights = _gameNightRepository.GetParticipating(user);
        
        return View(gameNights);
    }

    public IActionResult Participate()
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity!.Name!);
        
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);
    
        _gameNightRepository.Participate(id, user);
        
        return RedirectToAction("Participating"); 
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
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var gameNight = new GameNight
        {
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = new List<Game>(), Players = new List<User>(),
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        _gameNightRepository.AddGameNight(gameNight);

        return RedirectToAction(nameof(Organized));
    }

    public IActionResult Details()
    {
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var gameNight = _gameNightRepository.GetGameNightById(id);

        return View(gameNight);
    }

    [HttpGet]
    public IActionResult Update()
    {
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var gameNight = _gameNightRepository.GetGameNightById(id);

        var viewModel = new GameNightViewModel
        {
            City = gameNight.Address.City,
            Street = gameNight.Address.Street,
            HouseNumber = gameNight.Address.HouseNumber,
            Games = gameNight.Games ?? new List<Game>(),
            DateTime = gameNight.DateTime,
            IsPotluck = gameNight.IsPotluck,
            MaxPlayers = gameNight.MaxPlayers,
            IsOnlyForAdults = gameNight.IsOnlyForAdults
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Update(GameNightViewModel gameNightViewModel)
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var updatedGameNight = new GameNight
        {
            Id = id,
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Games = gameNightViewModel.Games,
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        _gameNightRepository.UpdateGameNight(updatedGameNight);

        return RedirectToAction("Organized");
    }

    public IActionResult Delete()
    {
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        _gameNightRepository.DeleteGameNight(id);

        return RedirectToAction("Organized");
    }
}