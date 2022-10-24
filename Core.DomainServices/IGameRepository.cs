using Core.Domain;

namespace Core.DomainServices;

public interface IGameRepository
{
    ICollection<Game> GetAllGames();
    Game GetGameById(int id);
    ICollection<Game> GetGamesByIds(List<int> gameIds);
}