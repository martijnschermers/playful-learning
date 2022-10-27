using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class GameEFRepository : IGameRepository
{
    private readonly DomainDbContext _context; 
    
    public GameEFRepository(DomainDbContext context, IDbContextFactory<DomainDbContext>? contextFactory)
    {
        _context = context; 
        if (contextFactory != null) _context = contextFactory.CreateDbContext();
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