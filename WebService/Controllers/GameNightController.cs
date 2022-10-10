using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameNightController : ControllerBase
{
    private readonly IGameNightRepository _repository;
    private readonly ILogger<GameNightController> _logger;

    public GameNightController(ILogger<GameNightController> logger, IGameNightRepository repository)
    {
        _logger = logger;
        _repository = repository; 
    }

    [HttpGet(Name = "GetGameNight")]
    public IEnumerable<GameNight> Get()
    {
        return _repository.GetAllGameNights(); 
    }
}