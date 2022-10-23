using Core.Domain;

namespace Core.DomainServices;

public class GameNightService : IGameNightService
{
    private readonly IGameNightRepository _repository;
    
    public GameNightService(IGameNightRepository repository)
    {
        _repository = repository;
    }

    //TODO: Validate if requesting user is organizer, for deleting as well 
    public string UpdateGameNight(int id, GameNight updatedGameNight)
    {
        var originalGameNight = _repository.GetGameNightById(id);

        if (originalGameNight == null) {
            return "Spelavond niet gevonden.";
        }
        
        // When there are participants, updating is not allowed 
        if (originalGameNight.Players.Count > 0) {
            return "Het is niet toegestaan om de spelavond aan te passen, omdat er al deelnemers zijn.";
        }
        
        _repository.UpdateGameNight(originalGameNight, updatedGameNight);
        return ""; 
    }

    public string DeleteGameNight(int gameNightId)
    {
        var gameNight = _repository.GetGameNightById(gameNightId);

        if (gameNight == null) {
            return "Spelavond niet gevonden.";
        }
        
        // When there are participants, deleting is not allowed 
        if (gameNight.Players.Count > 0) {
            return "Het is niet toegestaan om de spelavond te verwijderen, omdat er al deelnemers zijn.";
        }
        
        _repository.DeleteGameNight(gameNight);
        return ""; 
    }
    
    public string Participate(GameNight gameNight, User user)
    {
        var age = user.GetAge(null);

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