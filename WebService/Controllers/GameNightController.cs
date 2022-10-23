using ApplicationServices;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Post([FromBody] GameNight gameNight)
    {
        _repository.AddGameNight(gameNight);
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

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] GameNight gameNight)
    {
        var user = _helperService.GetUser(HttpContext);
        var result = _gameNightService.UpdateGameNight(id, gameNight, user);

        if (result != "") {
            return BadRequest(new { Message = result });
        }
        
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = _helperService.GetUser(HttpContext);
        var result = _gameNightService.DeleteGameNight(id, user);

        if (result != "") {
            return BadRequest(new { Message = result });
        }

        return Ok(new { Message = "Succesvol verwijderd." });
    }
}