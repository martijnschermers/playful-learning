using ApplicationServices;
using Core.Domain;
using Core.DomainServices;
using WebService.Models;

namespace WebService.GraphQL;

public class Mutation
{
    public Task<string> SignIn([Service] IIdentityService<string> identityService, string email, string password)
    {
        return identityService.SignIn(new AuthenticationCredentials(email, password));
    }

    public GameNightPayload AddGameNight([Service] IGameNightService service, [Service] IUserRepository repository, GameNightViewModel gameNightViewModel)
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
}