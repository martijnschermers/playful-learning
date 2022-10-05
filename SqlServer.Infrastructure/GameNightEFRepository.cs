using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

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
        return _context.GameNights.Include(g => g.Address).Include(g => g.Organizer).ToList(); 
    }

    public void AddGameNight(GameNight gameNight)
    {
        _context.GameNights.Add(gameNight);
        _context.SaveChanges(); 
    }

    public GameNight GetGameNightById(int id)
    {
        return _context.GameNights.First(g => g.Id == id); 
    }
}