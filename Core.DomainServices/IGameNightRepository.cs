using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightRepository
{
    ICollection<GameNight> GetAllGameNights();
    void AddGameNight(GameNight gameNight);
    GameNight GetGameNightById(int id); 
}