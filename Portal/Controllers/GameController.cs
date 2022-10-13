using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers;

public class GameController : Controller
{
    private readonly IGameRepository _repository;

    public GameController(IGameRepository repository)
    {
        _repository = repository;
    }

    public Game GetGameById(int id)
    {
        return _repository.GetGameById(id); 
    }

    public ICollection<Game> GetAllGames()
    {
        return _repository.GetAllGames();
    }
}