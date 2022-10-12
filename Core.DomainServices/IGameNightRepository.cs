using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightRepository
{
    ICollection<GameNight> GetAllGameNights();
    ICollection<GameNight> GetParticipating(User user);
    void AddGameNight(GameNight gameNight);
    GameNight GetGameNightById(int id);
    void UpdateGameNight(GameNight updatedGameNight);
    void DeleteGameNight(int gameNightId);
    string Participate(int gameNightId, User user); 
}