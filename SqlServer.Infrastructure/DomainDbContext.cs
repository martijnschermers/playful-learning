using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class DomainDbContext : DbContext
{ 
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options) { }
    
    public DbSet<GameNight> GameNights { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Allergy> Allergies { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var games = new List<Game>
        {
            new Game { Id = 1, Name = "Uno", Description = "Versimpelde versie van pesten!", Genre = Genre.Kids, Image = "", Type = GameType.Card, IsOnlyForAdults = false }, 
            new Game { Id = 2, Name = "Ticket to Ride", Description = "Het spel met treinen!", Genre = Genre.Kids, Image = "", Type = GameType.Board, IsOnlyForAdults = false }, 
            new Game { Id = 3, Name = "Monopoly", Description = "Het spel met geld.", Genre = Genre.Kids, Image = "", Type = GameType.Board, IsOnlyForAdults = false }
        };

        var allergies = new List<Allergy>
        {
            new Allergy { Id = 1, Name = AllergyEnum.Gluten, Description = "Gluten"},
            new Allergy { Id = 2, Name = AllergyEnum.Lactose, Description = "Lactose"},
            new Allergy { Id = 3, Name = AllergyEnum.Nuts, Description = "Noten"},
            new Allergy { Id = 4, Name = AllergyEnum.Soya, Description = "Soja" },
            new Allergy { Id = 5, Name = AllergyEnum.Wheat, Description = "Tarwe"},
            new Allergy { Id = 6, Name = AllergyEnum.Vegan, Description = "Vegetarisch"}
        };

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasData(games);
        modelBuilder.Entity<Allergy>().HasData(allergies);
    }
}