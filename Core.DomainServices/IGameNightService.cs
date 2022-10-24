using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightService
{
    void AddGameNight(GameNight gameNight);
    string Participate(GameNight gameNight, User user);
    string DeleteGameNight(int gameNightId);
    string UpdateGameNight(int id, GameNight updatedGameNight); 
}