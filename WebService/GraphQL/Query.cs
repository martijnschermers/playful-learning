using Core.Domain;
using Core.DomainServices;
using HotChocolate.AspNetCore.Authorization;

namespace WebService.GraphQL;

[Authorize]
public class Query
{
    public GameNight? GetGameNightById([Service] IGameNightRepository repository, int id)
    {
        var gameNight = repository.GetGameNightById(id);

        return gameNight ?? null;
    }
    
    public ICollection<GameNight> GetAllGameNights([Service] IGameNightRepository repository)
    {
        return repository.GetAllGameNights();
    }

    public ICollection<Game> GetAllGames([Service] IGameRepository repository)
    {
        return repository.GetAllGames();
    }
}