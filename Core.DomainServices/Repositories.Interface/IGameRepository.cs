using Core.Domain;

namespace Core.DomainServices.Repositories.Interface;

public interface IGameRepository
{
    ICollection<Game> GetAllGames();
    Game GetGameById(int id);
    ICollection<Game> GetGamesByIds(List<int> gameIds);
}