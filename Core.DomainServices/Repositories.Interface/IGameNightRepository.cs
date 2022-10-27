using Core.Domain;

namespace Core.DomainServices.Repositories.Interface;

public interface IGameNightRepository
{
    ICollection<GameNight> GetAllGameNights();
    ICollection<GameNight> GetPopularGameNights(); 
    GameNight? GetGameNightById(int id);
    ICollection<GameNight> GetParticipating(User user);
    ICollection<GameNight> GetOrganized(User user);
    void AddGameNight(GameNight gameNight);
    void UpdateGameNight(GameNight originalGameNight, GameNight updatedGameNight);
    void DeleteGameNight(GameNight gameNight);
    void Participate(GameNight gameNight, User user);
    bool AddFood(int id, Food food); 
}