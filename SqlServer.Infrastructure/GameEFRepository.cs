using Core.Domain;
using Core.DomainServices;

namespace SqlServer.Infrastructure;

public class GameEFRepository : IGameRepository
{
    private readonly DomainDbContext _context; 
    
    public GameEFRepository(DomainDbContext context)
    {
        _context = context; 
    }
    
    public ICollection<Game> GetAllGames()
    {
        return _context.Games.ToList();
    }

    public Game GetGameById(int id)
    {
        return _context.Games.Find(id)!;
    }

    public ICollection<Game> GetGamesByIds(List<int> gameIds)
    {
        return gameIds
            .Select(gameId => GetGameById(gameId))
            .ToList();
    }
}