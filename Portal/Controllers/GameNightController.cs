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
    private readonly IGameRepository? _gameRepository;
    private readonly IUserRepository? _userRepository;
    private readonly IGameNightService? _gameNightService;

    public GameNightController(IGameNightRepository gameNightRepository, IGameRepository? gameRepository,
        IUserRepository? userRepository, IGameNightService? gameNightService)
    {
        _gameNightRepository = gameNightRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
        _gameNightService = gameNightService;
    }
    
    public IActionResult Index()
    {
        var gameNights = _gameNightRepository.GetAllGameNights();

        return View(gameNights);
    }

    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organized()
    {
        var user = GetUser();

        var gameNights = _gameNightRepository.GetAllGameNights().Where(g => g.Organizer == user);

        return View(gameNights);
    }

    public IActionResult Participating()
    {
        var user = GetUser();
        
        var gameNights = _gameNightRepository.GetParticipating(user);
        
        return View(gameNights);
    }

    [HttpGet]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize()
    {
        return View(new GameNightViewModel{ Games = _gameRepository!.GetAllGames() });
    }

    [HttpPost]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize(GameNightViewModel gameNightViewModel)
    {
        if (!ModelState.IsValid) {
            return View(new GameNightViewModel { Games = _gameRepository!.GetAllGames() });
        }

        var gameIds = Request.Form.First(f => f.Key == "Game");
        var games = gameIds.Value.Select(gameId => _gameRepository!.GetGameById(int.Parse(gameId))).ToList();

        var isForAdults = gameNightViewModel.IsOnlyForAdults || games.Any(g => g.IsOnlyForAdults);

        var user = GetUser();

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
            IsOnlyForAdults = isForAdults, Organizer = user
        };

        _gameNightRepository.AddGameNight(gameNight);

        return RedirectToAction(nameof(Organized));
    }

    [HttpGet]
    public IActionResult Details()
    {
        var id = GetId();

        var gameNight = _gameNightRepository.GetGameNightById(id);

        return View(gameNight);
    }

    [HttpPost]
    public IActionResult Details(GameNight gameNight)
    {
        var user = GetUser();

        var id = GetId();

        gameNight = _gameNightRepository.GetGameNightById(id);

        var result = _gameNightService!.Participate(gameNight, user);

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
        
        var id = GetId();

        var gameNight = _gameNightRepository.GetGameNightById(id);
        var games = _gameRepository!.GetAllGames();

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
        var user = GetUser();
        var id = GetId();

        //TODO: Maybe move this logic to GameNightService?? 
        var gameIds = Request.Form.First(f => f.Key == "Game");
        var games = gameIds.Value.Select(gameId => _gameRepository!.GetGameById(int.Parse(gameId))).ToList();
        
        var isForAdults = gameNightViewModel.IsOnlyForAdults || games.Any(g => g.IsOnlyForAdults);

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
            IsOnlyForAdults = isForAdults, Organizer = user
        };

        _gameNightService!.UpdateGameNight(updatedGameNight);

        return RedirectToAction(nameof(Organized));
    }

    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Delete()
    {
        var id = GetId();

        _gameNightService!.DeleteGameNight(id);

        return RedirectToAction(nameof(Organized));
    }
    
    // General methods
    public User GetUser()
    {
        var identity = HttpContext.User.Identity;
        return _userRepository!.GetUserByEmail(identity!.Name!);
    }

    public int GetId()
    {
        return int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);
    }
    
    // Methods for testing
    public void AddGameNight(GameNight gameNight)
    {
        _gameNightRepository.AddGameNight(gameNight);
    }
    
    public string Participate(GameNight gameNight, User user)
    {
        return _gameNightService!.Participate(gameNight, user);
    }
    
    public GameNight GetGameNightById(int id)
    {
        return _gameNightRepository.GetGameNightById(id);
    }

    public ICollection<GameNight> GetAllGameNights()
    {
        return _gameNightRepository.GetAllGameNights(); 
    }

    public ICollection<GameNight> GetParticipating(User user)
    {
        return _gameNightRepository.GetParticipating(user); 
    }
}