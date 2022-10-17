using Core.Domain;

namespace Core.DomainServices;

public interface IGameNightRepository
{
    ICollection<GameNight> GetAllGameNights();
    GameNight GetGameNightById(int id);
    ICollection<GameNight> GetParticipating(User user);
    void AddGameNight(GameNight gameNight);
    void UpdateGameNight(GameNight originalGameNight, GameNight updatedGameNight);
    void DeleteGameNight(GameNight gameNight);
    void Participate(GameNight gameNight, User user); 
}