using Core.Domain;
using Core.DomainServices.Repositories.Interface;
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
            .Include(g => g.Players)
            .ToList();
    }

    public ICollection<GameNight> GetPopularGameNights()
    {
        return GetAllGameNights()
            .OrderByDescending(g => g.Players.Count)
            .Take(3)
            .ToList();
    }

    public ICollection<GameNight> GetOrganized(User user)
    {
        return GetAllGameNights()
            .Where(g => g.OrganizerId == user.Id)
            .ToList();
    }

    public ICollection<GameNight> GetParticipating(User user)
    {
        return GetAllGameNights()
            .Where(g => g.Players.Contains(user))
            .ToList();
    }

    public void AddGameNight(GameNight gameNight)
    {
        _context.GameNights.Add(gameNight);
        _context.SaveChanges();
    }

    public GameNight? GetGameNightById(int id)
    {
        return _context.GameNights
            .Where(g => g.Id == id)
            .Include(g => g.Address)
            .Include(g => g.Games)
            .Include(g => g.Players)
            .Include(g => g.Drinks)
            .Include(g => g.Foods)
            .ThenInclude(f => f.Allergies)
            .FirstOrDefault();
    }

    public void UpdateGameNight(GameNight originalGameNight, GameNight updatedGameNight)
    {
        originalGameNight.MaxPlayers = updatedGameNight.MaxPlayers;
        originalGameNight.IsOnlyForAdults = updatedGameNight.IsOnlyForAdults;
        originalGameNight.IsPotluck = updatedGameNight.IsPotluck;
        originalGameNight.DateTime = updatedGameNight.DateTime;
        originalGameNight.OrganizerId = updatedGameNight.OrganizerId;
        originalGameNight.Games = updatedGameNight.Games;
        originalGameNight.Address = updatedGameNight.Address;

        _context.SaveChanges();
    }

    public void DeleteGameNight(GameNight gameNight)
    {
        _context.GameNights.Remove(gameNight);
        _context.SaveChanges();
    }

    public void Participate(GameNight gameNight, User user)
    {
        gameNight.Players.Add(user);
        user.GameNights.Add(gameNight);
        _context.SaveChanges();
    }

    public bool AddFood(int id, Food food)
    {
        var gameNight = GetGameNightById(id);

        if (gameNight == null) {
            return false;
        }

        gameNight.Foods.Add(food);

        _context.SaveChanges();
        return true;
    }
}