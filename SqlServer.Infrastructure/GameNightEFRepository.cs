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

    public ICollection<GameNight> GetParticipating(User user)
    {
        return _context.GameNights.Where(g => g.Players.Contains(user))  
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

    public void UpdateGameNight(GameNight updatedGameNight)
    {
        var originalGameNight = _context.GameNights.FirstOrDefault(g => g.Id == updatedGameNight.Id)!;

        // When there are participants, updating is not allowed 
        if (originalGameNight.Players.Count > 0) {
            return;
        }

        originalGameNight.MaxPlayers = updatedGameNight.MaxPlayers;
        originalGameNight.IsOnlyForAdults = updatedGameNight.IsOnlyForAdults;
        originalGameNight.IsPotluck = updatedGameNight.IsPotluck;
        originalGameNight.DateTime = updatedGameNight.DateTime;
        originalGameNight.Organizer = updatedGameNight.Organizer;
        originalGameNight.Games = updatedGameNight.Games;
        originalGameNight.Address = updatedGameNight.Address;

        _context.SaveChanges();
    }

    public void DeleteGameNight(int gameNightId)
    {
        var gameNight = _context.GameNights
            .Where(g => g.Id == gameNightId)
            .Include(g => g.Players)
            .First();

        // When there are participants, deleting is not allowed 
        if (gameNight.Players.Count > 0) {
            return;
        }

        _context.GameNights.Remove(gameNight);
        _context.SaveChanges();
    }

    public void Participate(int gameNightId, User user)
    {
        var gameNight = _context.GameNights
            .Where(g => g.Id == gameNightId)
            .Include(g => g.Players)
            .First();    
        
        gameNight.Players.Add(user);

        _context.SaveChanges();
    }
}