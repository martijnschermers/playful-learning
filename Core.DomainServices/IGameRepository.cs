using Core.Domain;

namespace Core.DomainServices;

public interface IGameRepository
{
    ICollection<Game> GetAllGames(); 
}