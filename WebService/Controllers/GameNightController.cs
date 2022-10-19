using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameNightController : ControllerBase
{
    [HttpGet(Name = "GetAllGameNights")]
    public IEnumerable<GameNight> Get([Service] IGameNightRepository repository)
    {
        return repository.GetAllGameNights(); 
    }
}