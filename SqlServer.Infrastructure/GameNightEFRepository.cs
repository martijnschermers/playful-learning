using Core.Domain;
using Core.DomainServices;

namespace SqlServer.Infrastructure;

public class GameNightEFRepository : IGameNightRepository
{
    private readonly DomainDbContext _context; 
    
    public GameNightEFRepository(DomainDbContext context)
    {
        _context = context; 
    }
    
    public ICollection<GameNight> GetAllGameNights()
    {
        return _context.GameNights.ToList(); 
    }

    public void AddGameNight(GameNight gameNight)
    {
        _context.GameNights.Add(gameNight);
        _context.SaveChanges(); 
    }
}