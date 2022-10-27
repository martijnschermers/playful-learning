using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Core.DomainServices.Services.Interface;
using WebService.Models;

namespace WebService.GraphQL;

public class Mutation
{
    private readonly IUserRepository _userRepository;
    private readonly IGameNightService _service;

    public Mutation(IUserRepository userRepository, IGameNightService service)
    {
        _userRepository = userRepository;
        _service = service;
    }

    public GameNightPayload AddGameNight(GameNightViewModel gameNightViewModel)
    {
        var user = _userRepository.GetUserById(gameNightViewModel.OrganizerId!.Value);

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
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, OrganizerId = user.Id
        };

        _service.AddGameNight(gameNight);

        return new GameNightPayload { GameNight = gameNight, Message = "Succesvol toegevoegd!" };
    }

    public GameNightPayload UpdateGameNight(GameNightViewModel gameNightViewModel, int id)
    {
        var user = _userRepository.GetUserById(gameNightViewModel.OrganizerId!.Value);

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
            IsOnlyForAdults = gameNightViewModel.IsOnlyForAdults, OrganizerId = user.Id
        };

        var result = _service.UpdateGameNight(id, gameNight);

        if (result != "") {
            return new GameNightPayload { Message = result };
        }

        return new GameNightPayload { GameNight = gameNight, Message = "Succesvol aangepast!" };
    }

    public GameNightPayload DeleteGameNight(int id)
    {
        var result = _service.DeleteGameNight(id);

        if (result != "") {
            return new GameNightPayload { Message = result };
        }

        return new GameNightPayload { Message = "Succesvol verwijderd!" };
    }

    // public Task<string> SignIn([Service] IIdentityService<string> identityService, string email, string password)
    // {
    //     return identityService.SignIn(new AuthenticationCredentials(email, password));
    // }
}