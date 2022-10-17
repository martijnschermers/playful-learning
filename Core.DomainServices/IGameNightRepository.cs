using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightRepository
{
    ICollection<GameNight> GetAllGameNights();
    GameNight GetGameNightById(int id);
    ICollection<GameNight> GetParticipating(User user);
    void AddGameNight(GameNight gameNight);
    void UpdateGameNight(GameNight updatedGameNight);
    void DeleteGameNight(int gameNightId);
    void Participate(GameNight gameNight, User user); 
}