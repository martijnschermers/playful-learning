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
        return _context.GameNights
            .Include(g => g.Address)
            .Include(g => g.Organizer)
            .Include(g => g.Players)
            .ToList();
    }

    public void AddGameNight(GameNight gameNight)
    {
        _context.GameNights.Add(gameNight);
        _context.SaveChanges();
    }

    public GameNight GetGameNightById(int id)
    {
        return _context.GameNights
            .Where(g => g.Id == id)
            .Include(g => g.Address)
            .Include(g => g.Organizer)
            .Include(g => g.Players)
            .First();
    }

    public async void UpdateGameNight(GameNight originalGameNight)
    {
        _context.Entry(await _context.GameNights
                .FirstOrDefaultAsync(x => x.Id == originalGameNight.Id))
                .CurrentValues
                .SetValues(originalGameNight);

        await _context.SaveChangesAsync(); 
    }
}