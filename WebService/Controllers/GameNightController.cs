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
    private readonly IUserRepository _userRepository;
    private readonly IGameNightService _gameNightService;

    public GameNightController(IGameNightRepository repository, IUserRepository userRepository, IGameNightService gameNightService)
    {
        _repository = repository;
        _userRepository = userRepository;
        _gameNightService = gameNightService;
    }

    [HttpGet]
    public ICollection<GameNight> Get()
    {
        return _repository.GetAllGameNights();
    }

    [HttpGet("{id}")]
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

    [HttpGet("{id}/participate")]
    public IActionResult Participate(int id)
    {
        var user = GetUser(); 
        
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
    
    public User GetUser()
    {
        var identity = HttpContext.User.Identity;
        return _userRepository!.GetUserByEmail(identity!.Name!);
    }
}