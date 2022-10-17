using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightService
{
    string Participate(GameNight gameNight, User user);
    void DeleteGameNight(int gameNightId);
    void UpdateGameNight(GameNight updatedGameNight); 
}