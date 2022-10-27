using ApplicationServices;
using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Core.DomainServices.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GameNightController : ControllerBase
{
    private readonly IGameNightRepository _repository;
    private readonly IHelperService _helperService;
    private readonly IGameNightService _gameNightService;

    public GameNightController(IGameNightRepository repository, IHelperService helperService,
        IGameNightService gameNightService)
    {
        _repository = repository;
        _helperService = helperService;
        _gameNightService = gameNightService;
    }

    [HttpGet]
    public ICollection<GameNight> Get()
    {
        return _repository.GetAllGameNights();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var gameNight = _repository.GetGameNightById(id);

        if (gameNight != null) {
            return Ok(gameNight);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult Post([FromBody] GameNightViewModel gameNightViewModel)
    {
        var user = _helperService.GetUser(HttpContext);

        var gameNight = new GameNight
        {
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = gameNightViewModel.Games,
            Players = new List<User>(),
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        _gameNightService.AddGameNight(gameNight);
        return Ok(gameNight);
    }

    [HttpGet("{id:int}/participate")]
    public IActionResult Participate(int id)
    {
        var user = _helperService.GetUser(HttpContext);

        var gameNight = _repository.GetGameNightById(id);

        if (gameNight == null) {
            return NotFound();
        }

        var result = _gameNightService.Participate(gameNight, user);

        if (result != "") {
            return Problem(result, null, 400);
        }

        return Ok(new { Message = "Succesvol deelgenomen." });
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] GameNightViewModel gameNightViewModel)
    {
        var user = _helperService.GetUser(HttpContext);

        var gameNight = new GameNight
        {
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

        var result = _gameNightService.UpdateGameNight(id, gameNight);

        if (result != "") {
            return BadRequest(new { Message = result });
        }

        return Ok(new { Message = "Succesvol aangepast." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _gameNightService.DeleteGameNight(id);

        if (result != "") {
            return BadRequest(new { Message = result });
        }

        return Ok(new { Message = "Succesvol verwijderd." });
    }
}