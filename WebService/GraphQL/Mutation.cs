using ApplicationServices;
using Core.Domain;
using Core.DomainServices;
using WebService.Models;

namespace WebService.GraphQL;

public class Mutation
{ 
    public GameNightPayload AddGameNight([Service] IGameNightService service, [Service] IUserRepository repository,
        GameNightViewModel gameNightViewModel)
    {
        var user = repository.GetUserById(gameNightViewModel.OrganizerId!.Value);

        if (user == null) {
            return new GameNightPayload { Message = "Organisator met dat id bestaat niet!" };
        }

        var gameNight = new GameNight
        {
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Drinks = new List<Drink>(), Foods = new List<Food>(), Games = gameNightViewModel.Games,
            Players = new List<User>(),
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        service.AddGameNight(gameNight);

        return new GameNightPayload { GameNight = gameNight, Message = "Succesvol toegevoegd!" };
    }
    
    public GameNightPayload UpdateGameNight([Service] IGameNightService service, [Service] IUserRepository repository,
        GameNightViewModel gameNightViewModel, int id)
    {
        var user = repository.GetUserById(gameNightViewModel.OrganizerId!.Value);

        if (user == null) {
            return new GameNightPayload { Message = "Organisator met dat id bestaat niet!" };
        }

        var gameNight = new GameNight
        {
            Address = new Address
            {
                Street = gameNightViewModel.Street, City = gameNightViewModel.City,
                HouseNumber = gameNightViewModel.HouseNumber
            },
            Games = gameNightViewModel.Games,
            DateTime = gameNightViewModel.DateTime, IsPotluck = gameNightViewModel.IsPotluck,
            MaxPlayers = gameNightViewModel.MaxPlayers,
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, Organizer = user
        };

        var result = service.UpdateGameNight(id, gameNight);

        if (result != "") {
            return new GameNightPayload { Message = result };
        }

        return new GameNightPayload { GameNight = gameNight, Message = "Succesvol aangepast!" };
    }

    public GameNightPayload DeleteGameNight([Service] IGameNightService service, int id)
    {
        var result = service.DeleteGameNight(id);

        if (result != "") {
            return new GameNightPayload { Message = result };
        }

        return new GameNightPayload { Message = "Succesvol verwijderd!" };
    }
    
    //TODO: Implement participate mutation
    
    public Task<string> SignIn([Service] IIdentityService<string> identityService, string email, string password)
    {
        return identityService.SignIn(new AuthenticationCredentials(email, password));
    }
}
