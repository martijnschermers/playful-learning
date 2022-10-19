using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameNightController : ControllerBase
{
    private readonly IGameNightRepository _repository;

    public GameNightController(IGameNightRepository repository)
    {
        _repository = repository; 
    }

    [HttpGet(Name = "GetAllGameNights")]
    public IEnumerable<GameNight> Get()
    {
        return _repository.GetAllGameNights(); 
    }
}