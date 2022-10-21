using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightService
{
    string Participate(GameNight gameNight, User user);
    string DeleteGameNight(int gameNightId);
    string UpdateGameNight(int id, GameNight updatedGameNight); 
}