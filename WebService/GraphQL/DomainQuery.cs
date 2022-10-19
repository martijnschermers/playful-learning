using Core.Domain;
using Core.DomainServices;

namespace WebService.GraphQL;

public class DomainQuery
{
    public GameNight GetGameNightById([Service] IGameNightRepository repository, int id)
    {
        return repository.GetGameNightById(id); 
    }
    
    public ICollection<GameNight> GetAllGameNights([Service] IGameNightRepository repository)
    {
        return repository.GetAllGameNights();
    }
}