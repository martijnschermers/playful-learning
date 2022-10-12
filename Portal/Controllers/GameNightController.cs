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

    [Authorize(Policy = "OnlyOrganizers")]
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

    [HttpGet]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize()
    {
        var games = _gameRepository.GetAllGames();
                
        return View(new GameNightViewModel(games));
    }

    [HttpPost]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize(GameNightViewModel gameNightViewModel)
    {
        if (!ModelState.IsValid) {
            return View(gameNightViewModel);
        }

        var games = new List<Game>(); 
        var gameIds = Request.Form.First(f => f.Key == "Game");
        foreach (var gameId in gameIds.Value) {
            games.Add(_gameRepository.GetGameById(int.Parse(gameId))); 
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
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = games, Players = new List<User>(),
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        _gameNightRepository.AddGameNight(gameNight);

        return RedirectToAction(nameof(Organized));
    }

    [HttpGet]
    public IActionResult Details()
    {
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var gameNight = _gameNightRepository.GetGameNightById(id);

        return View(gameNight);
    }

    [HttpPost]
    public IActionResult Details(GameNight gameNight)
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        gameNight = _gameNightRepository.GetGameNightById(id); 
    
        var result = _gameNightRepository.Participate(id, user);

        if (result != "") {
            ModelState.AddModelError("", result);
            return View(gameNight);
        }
        
        return RedirectToAction(nameof(Participating)); 
    }

    [HttpGet]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Update()
    {
        TempData.Clear();
        
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var gameNight = _gameNightRepository.GetGameNightById(id);
        var games = _gameRepository.GetAllGames();

        foreach (var game in games) {
            if (gameNight.Games.Contains(game)) {
                TempData.Add(game.Id.ToString(), game.Name);
            }
        }

        var viewModel = new GameNightViewModel
        {
            City = gameNight.Address.City,
            Street = gameNight.Address.Street,
            HouseNumber = gameNight.Address.HouseNumber,
            Games = games,
            DateTime = gameNight.DateTime,
            IsPotluck = gameNight.IsPotluck,
            MaxPlayers = gameNight.MaxPlayers,
            IsOnlyForAdults = gameNight.IsOnlyForAdults
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Update(GameNightViewModel gameNightViewModel)
    {
        var identity = HttpContext.User.Identity;
        var user = _userRepository.GetUserByEmail(identity!.Name!);

        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        var gameIds = Request.Form.First(f => f.Key == "Game");
        var games = gameIds.Value.Select(gameId => _gameRepository.GetGameById(int.Parse(gameId))).ToList();

        var updatedGameNight = new GameNight
        {
            Id = id,
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Games = games,
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        _gameNightRepository.UpdateGameNight(updatedGameNight);

        return RedirectToAction(nameof(Organized));
    }

    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Delete()
    {
        var id = int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);

        _gameNightRepository.DeleteGameNight(id);

        return RedirectToAction(nameof(Organized));
    }
}