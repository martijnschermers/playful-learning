using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Core.DomainServices.Services.Interface;

namespace Core.DomainServices.Services.Implementation;

public class GameNightService : IGameNightService
{
    private readonly IGameNightRepository _repository;
    
    public GameNightService(IGameNightRepository repository)
    {
        _repository = repository;
    }

    public void AddGameNight(GameNight gameNight)
    {
        gameNight.IsOnlyForAdults = gameNight.IsOnlyForAdults || gameNight.Games.Any(g => g.IsOnlyForAdults);
        _repository.AddGameNight(gameNight);
    }

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
        
        updatedGameNight.IsOnlyForAdults = updatedGameNight.IsOnlyForAdults || updatedGameNight.Games.Any(g => g.IsOnlyForAdults);
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

        if (user.GameNights.Any(g => g.DateTime.Date == gameNight.DateTime.Date)) {
            return "Je mag je maar inschrijven voor 1 spelavond per dag!"; 
        }

        if (gameNight.Players.Count + 1 > gameNight.MaxPlayers) {
            return "Het is niet mogelijk om in te schrijven, omdat de spelavond vol is!";
        }
        
        if (gameNight.IsPotluck && gameNight.Foods.All(f => f.UserId != user.Id)) {
            return "Je moet op zijn minst een ding meenemen!";
        }

        if (user.Allergies.Any()) {
            if (!user.Allergies.Any(allergy => gameNight.Foods.Any(f => f.Allergies.Contains(allergy)))) {
                return "Uw allergie??n of dieetwensen sluiten niet aan op deze spelavond!";
            }
        }
        
        _repository.Participate(gameNight, user);
        return ""; 
    }
}