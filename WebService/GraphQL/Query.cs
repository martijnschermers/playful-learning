using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using HotChocolate.AspNetCore.Authorization;

namespace WebService.GraphQL;

[Authorize]
public class Query
{
    private readonly IGameNightRepository _gameNightRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public Query(IGameNightRepository gameNightRepository, IGameRepository gameRepository, IUserRepository userRepository)
    {
        _gameNightRepository = gameNightRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }
    
    public GameNight? GetGameNightById(int id)
    {
        var gameNight = _gameNightRepository.GetGameNightById(id);

        return gameNight ?? null;
    }
    
    public ICollection<GameNight> GetAllGameNights()
    {
        return _gameNightRepository.GetAllGameNights();
    }

    public ICollection<Game> GetAllGames()
    {
        return _gameRepository.GetAllGames();
    }

    public ICollection<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserByEmail(string email)
    {
        var user = _userRepository.GetUserByEmail(email);

        return user ?? null;
    }
}