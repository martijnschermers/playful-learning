using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class DomainDbContext : DbContext
{
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
    }

    public DbSet<GameNight> GameNights { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Allergy> Allergies { get; set; } = null!;
    public DbSet<Drink> Drinks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var games = new List<Game>
        {
            new Game
            {
                Id = 1, Name = "Uno", Description = "Versimpelde versie van pesten!", Genre = Genre.Kids, Image = "https://img.poki.com/cdn-cgi/image/quality=78,width=600,height=600,fit=cover,f=auto/26c6e4e18eeaa62590fccd44ea7812f8.png",
                Type = GameType.Card, IsOnlyForAdults = false
            },
            new Game
            {
                Id = 2, Name = "Ticket to Ride", Description = "Het spel met treinen!", Genre = Genre.Kids, Image = "https://media.s-bol.com/4zrr6XMkgVXn/550x536.jpg",
                Type = GameType.Board, IsOnlyForAdults = false
            },
            new Game
            {
                Id = 3, Name = "Monopoly", Description = "Het spel met geld!", Genre = Genre.Kids, Image = "https://www.bruna.nl/images/active/carrousel/fullsize/5010993414338_front.jpg",
                Type = GameType.Board, IsOnlyForAdults = true
            }, 
            new Game
            {
                Id = 4, Name = "30 Seconds", Description = "Beantwoord zoveel mogelijk vragen in 30 seconde!", Genre = Genre.Kids, Image = "https://play-lh.googleusercontent.com/vLezygtbLfIe6fi23WCg9Mc4jZn2CW1_6EWBraSCukUGsIpPaBQ7yUN14x4SVggzh3g",
                Type = GameType.Board, IsOnlyForAdults = true
            }
        };

        var allergies = new List<Allergy>
        {
            new Allergy { Id = 1, Name = AllergyEnum.Gluten, Description = "Gluten" },
            new Allergy { Id = 2, Name = AllergyEnum.Lactose, Description = "Lactose" },
            new Allergy { Id = 3, Name = AllergyEnum.Nuts, Description = "Noten" },
            new Allergy { Id = 4, Name = AllergyEnum.Soya, Description = "Soja" },
            new Allergy { Id = 5, Name = AllergyEnum.Wheat, Description = "Tarwe" },
            new Allergy { Id = 6, Name = AllergyEnum.Vegan, Description = "Vegetarisch" }
        };

        var drinks = new List<Drink>
        {
            new Drink { Id = 1, Name = "Bier", ContainsAlcohol = true },
            new Drink { Id = 2, Name = "Water", ContainsAlcohol = false },
            new Drink { Id = 3, Name = "Cola", ContainsAlcohol = false },
            new Drink { Id = 4, Name = "Wijn", ContainsAlcohol = true },
            new Drink { Id = 5, Name = "Fanta", ContainsAlcohol = false }
        };

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasData(games);
        modelBuilder.Entity<Allergy>().HasData(allergies);
        modelBuilder.Entity<Drink>().HasData(drinks);
    }
}