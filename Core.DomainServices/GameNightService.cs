using Core.Domain;

namespace Core.DomainServices;

public class GameNightService : IGameNightService
{
    private readonly IGameNightRepository _repository;
    
    public GameNightService(IGameNightRepository repository)
    {
        _repository = repository;
    }

    public void UpdateGameNight(GameNight updatedGameNight)
    {
        var originalGameNight = _repository.GetGameNightById(updatedGameNight.Id)!; 
        
        // When there are participants, updating is not allowed 
        if (originalGameNight.Players.Count > 0) {
            return;
        }
        
        _repository.UpdateGameNight(originalGameNight, updatedGameNight);
    }

    public void DeleteGameNight(int gameNightId)
    {
        var gameNight = _repository.GetGameNightById(gameNightId)!;
        
        // When there are participants, deleting is not allowed 
        if (gameNight.Players.Count > 0) {
            return;
        }
        
        _repository.DeleteGameNight(gameNight);
    }
    
    public string Participate(GameNight gameNight, User user)
    {
        var age = user.GetAge();

        if (age < 18 && gameNight.IsOnlyForAdults) {
            return "Het is niet toegestaan om deel te nemen aan een spelavond voor volwassenen als iemand jonger dan 18 jaar!";
        }

        if (gameNight.Players.Count + 1 > gameNight.MaxPlayers) {
            return "Het is niet mogelijk om in te schrijven, omdat de spelavond vol is!";
        }

        _repository.Participate(gameNight, user);
        return ""; 
    }
}