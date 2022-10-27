using ApplicationServices;
using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Core.DomainServices.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;

namespace Portal.Controllers;

[Authorize]
public class GameNightController : Controller
{
    private readonly IGameNightRepository _gameNightRepository;
    private readonly IGameRepository? _gameRepository;
    private readonly IHelperService? _helperService;
    private readonly IGameNightService? _gameNightService;

    public GameNightController(IGameNightRepository gameNightRepository, IGameRepository? gameRepository,
        IHelperService? helperService, IGameNightService? gameNightService)
    {
        _gameNightRepository = gameNightRepository;
        _gameRepository = gameRepository;
        _helperService = helperService;
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
        var user = _helperService!.GetUser(HttpContext);

        var gameNights = _gameNightRepository.GetOrganized(user);

        return View(gameNights);
    }

    public IActionResult Participating()
    {
        var user = _helperService!.GetUser(HttpContext);

        var gameNights = _gameNightRepository.GetParticipating(user);

        return View(gameNights);
    }

    [HttpGet]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize()
    {
        var games = _gameRepository!.GetAllGames()
            .Select(game => new CheckboxOption<Game>(false, game.Name, game))
            .ToList();
        
        HttpContext.Session.Clear();
        HttpContext.Session.SetString("Games", JsonConvert.SerializeObject(games));
        
        return View(new GameNightViewModel { Games = games });
    }

    [HttpPost]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Organize(GameNightViewModel gameNightViewModel)
    {
        if (!ModelState.IsValid) {
            var checkboxOptions = JsonConvert.DeserializeObject<List<CheckboxOption<Game>>>(HttpContext.Session.GetString("Games")!)!;

            return View(new GameNightViewModel { Games = checkboxOptions });
        }

        var games = _gameRepository!.GetGamesByIds(gameNightViewModel.Game);

        var user = _helperService!.GetUser(HttpContext);

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
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, OrganizerId = user.Id
        };

        _gameNightService!.AddGameNight(gameNight);

        return RedirectToAction(nameof(Organized));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var gameNight = _gameNightRepository.GetGameNightById(id);

        ViewBag.Organizer = _helperService!.GetUserById(gameNight!.OrganizerId);
        
        return View(gameNight);
    }

    [HttpPost]
    public ActionResult Details(int id, GameNight gameNight)
    {
        var user = _helperService!.GetUser(HttpContext);

        gameNight = _gameNightRepository.GetGameNightById(id)!;

        var result = _gameNightService!.Participate(gameNight, user);

        if (result != "") {
            ViewBag.Organizer = _helperService!.GetUserById(gameNight!.OrganizerId);
            ModelState.AddModelError("", result);
            return View(gameNight);
        }

        return RedirectToAction(nameof(Participating));
    }

    [HttpGet]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Update(int id)
    {
        var gameNight = _gameNightRepository.GetGameNightById(id)!;
        var games = _gameRepository!.GetAllGames();

        var checkBoxes = new List<CheckboxOption<Game>>();
        foreach (var game in games) {
            var isChecked = gameNight.Games.Contains(game);

            checkBoxes.Add(new CheckboxOption<Game>(isChecked, game.Name, game));
        }
        
        var viewModel = new GameNightViewModel
        {
            City = gameNight.Address.City,
            Street = gameNight.Address.Street,
            HouseNumber = gameNight.Address.HouseNumber,
            Games = checkBoxes,
            DateTime = gameNight.DateTime,
            IsPotluck = gameNight.IsPotluck,
            MaxPlayers = gameNight.MaxPlayers,
            IsOnlyForAdults = gameNight.IsOnlyForAdults
        };

        HttpContext.Session.Clear();
        HttpContext.Session.SetString("Checkboxes", JsonConvert.SerializeObject(checkBoxes));
        
        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Update(int id, GameNightViewModel gameNightViewModel)
    {
        var checkBoxes = JsonConvert.DeserializeObject<List<CheckboxOption<Game>>>(HttpContext.Session.GetString("Checkboxes")!)!;
        
        if (!ModelState.IsValid) {
            return View(new GameNightViewModel { Games = checkBoxes });
        }
        
        var user = _helperService!.GetUser(HttpContext);

        var games = _gameRepository!.GetGamesByIds(gameNightViewModel.Game);
        
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
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, OrganizerId = user.Id
        };

        var result = _gameNightService!.UpdateGameNight(id, updatedGameNight);

        if (result != "") {
            ModelState.AddModelError("", result);
            return View(new GameNightViewModel { Games = checkBoxes }); 
        }
        
        return RedirectToAction(nameof(Organized));
    }

    [Authorize(Policy = "OnlyOrganizers")]
    public IActionResult Delete(int id)
    {
        _gameNightService!.DeleteGameNight(id);

        return RedirectToAction(nameof(Organized));
    }
    
    // Methods for testing
    public GameNight? GetGameNightById(int id)
    {
        return _gameNightRepository.GetGameNightById(id);
    }

    public ICollection<GameNight> GetAllGameNights()
    {
        return _gameNightRepository.GetAllGameNights();
    }
}