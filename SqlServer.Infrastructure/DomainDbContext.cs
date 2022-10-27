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
                Id = 1, Name = "Uno", Description = "Versimpelde versie van pesten!", Genre = Genre.Kids,
                Image =
                    "https://img.poki.com/cdn-cgi/image/quality=78,width=600,height=600,fit=cover,f=auto/26c6e4e18eeaa62590fccd44ea7812f8.png",
                Type = GameType.Card, IsOnlyForAdults = false
            },
            new Game
            {
                Id = 2, Name = "Ticket to Ride", Description = "Het spel met treinen!", Genre = Genre.Kids,
                Image = "https://media.s-bol.com/4zrr6XMkgVXn/550x536.jpg",
                Type = GameType.Board, IsOnlyForAdults = false
            },
            new Game
            {
                Id = 3, Name = "Monopoly", Description = "Het spel met geld!", Genre = Genre.Kids,
                Image = "https://www.bruna.nl/images/active/carrousel/fullsize/5010993414338_front.jpg",
                Type = GameType.Board, IsOnlyForAdults = true
            },
            new Game
            {
                Id = 4, Name = "30 Seconds", Description = "Beantwoord zoveel mogelijk vragen in 30 seconde!",
                Genre = Genre.Kids,
                Image =
                    "https://play-lh.googleusercontent.com/vLezygtbLfIe6fi23WCg9Mc4jZn2CW1_6EWBraSCukUGsIpPaBQ7yUN14x4SVggzh3g",
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

        var foods = new List<Food>
        {
            new Food { Id = 1, Allergies = new List<Allergy> { allergies[5] }, Name = "Chips" },
            new Food { Id = 2, Allergies = new List<Allergy>(), Name = "Gevulde eitjes" },
            new Food { Id = 3, Allergies = new List<Allergy> { allergies[2] }, Name = "Borrelnootjes" },
            new Food { Id = 4, Allergies = new List<Allergy>(), Name = "Kip spiesjes" },
            new Food
            {
                Id = 5, Allergies = new List<Allergy> { allergies[0], allergies[1], allergies[3] }, Name = "Frikandel"
            },
        };

        var gameNights = new List<GameNight>
        {
            new GameNight
            {
                Id = 1, OrganizerId = 1, IsOnlyForAdults = false, Drinks = new List<Drink> { drinks[0], drinks[1] },
                Games = new List<Game> { games[0], games[1] },
                Address = new Address { Id = 1, City = "Breda", Street = "Lovensdijkstraat", HouseNumber = 61 },
                AddressId = 1,
                Foods = new List<Food> { foods[0], foods[1] }, DateTime = DateTime.Now.AddDays(30), IsPotluck = false,
                MaxPlayers = 10, Players = new List<User>()
            },
            new GameNight
            {
                Id = 2, OrganizerId = 1, IsOnlyForAdults = true, Drinks = new List<Drink> { drinks[1], drinks[3] },
                Games = new List<Game> { games[0], games[3] },
                Address = new Address { Id = 2, City = "Zaltbommel", Street = "Dorpstraat", HouseNumber = 10 },
                AddressId = 2,
                Foods = new List<Food> { foods[3], foods[1], foods[2] }, DateTime = DateTime.Now.AddDays(60), IsPotluck = true,
                MaxPlayers = 5, Players = new List<User>()
            },
            new GameNight
            {
                Id = 3, OrganizerId = 1, IsOnlyForAdults = false, Drinks = new List<Drink> { drinks[0], drinks[1] },
                Games = new List<Game> { games[0], games[1] },
                Address = new Address { Id = 3, City = "Breda", Street = "Lovensdijkstraat", HouseNumber = 61 },
                AddressId = 3,
                Foods = new List<Food> { foods[0], foods[1], foods[4] }, DateTime = DateTime.Now.AddDays(10), IsPotluck = false,
                MaxPlayers = 20, Players = new List<User>()
            },
        };

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasData(games);
        modelBuilder.Entity<Allergy>().HasData(allergies);
        modelBuilder.Entity<Drink>().HasData(drinks);
        // modelBuilder.Entity<GameNight>().HasData(gameNights);
    }
}