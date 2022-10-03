using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class DomainDbContext : DbContext
{
    public DbSet<GameNight> GameNights { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var games = new List<Game>
        {
            new Game { Id = 1, Name = "Uno", Description = "Versimpelde versie van pesten!", Genre = Genre.Kids, Image = "", Type = GameType.Card, IsOnlyForAdults = false }, 
            new Game { Id = 2, Name = "Ticket to Ride", Description = "Het spel met treinen!", Genre = Genre.Kids, Image = "", Type = GameType.Board, IsOnlyForAdults = false }, 
            new Game { Id = 3, Name = "Monopoly", Description = "Het spel met geld.", Genre = Genre.Kids, Image = "", Type = GameType.Board, IsOnlyForAdults = false }
        };

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasData(games);
    }
}