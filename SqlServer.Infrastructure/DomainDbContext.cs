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

        var address = new Address { City = "Breda", Id = 1, Street = "Lovensdijkstraat", HouseNumber = 61 };

        var drinks = new List<Drink>
        {
            new Drink { Id = 1, Name = "Bier", ContainsAlcohol = true },
            new Drink { Id = 2, Name = "Cola", ContainsAlcohol = false },
            new Drink { Id = 3, Name = "Wijn", ContainsAlcohol = true }
        };

        var allergies = new List<Allergy>
        {
            new Allergy { Id = 1, Name = AllergyEnum.Lactose },
            new Allergy { Id = 2, Name = AllergyEnum.Nuts },
        };

        var food = new List<Food>
        {
            new Food { Id = 1, Name = "Bitterballen", IsVegetarian = false, Allergies = allergies },
            new Food { Id = 2, Name = "Chips", IsVegetarian = true, Allergies = new List<Allergy>() },
            new Food { Id = 3, Name = "Nootjes", IsVegetarian = true, Allergies = allergies },
        };

        var users = new List<User>
        {
            new User
            {
                Id = 1, Allergies = allergies, Email = "martijnschermers.personal@gmail.com", Gender = Gender.Male,
                Name = "Martijn", BirthDate = new DateTime(2004, 3, 4)
            }, 
            new User
            {
                Id = 2, Allergies = new List<Allergy>(), Email = "keesklaasen@gmail.com", Gender = Gender.Male,
                Name = "Kees", BirthDate = new DateTime(1995, 3, 4)
            }, 
        };
        
        IEnumerable<GameNight> gameNights = new List<GameNight>
        {
            new GameNight { Id = 1, Drinks = drinks, Foods = food, Games = games, Organizer = users.First(), OrganizerId = users.First().Id, Players = users, DateTime = DateTime.Now, IsPotluck = false, MaxPlayers = 12 }, 
        }; 

        // base.OnModelCreating(modelBuilder);
        //
        // modelBuilder.Entity<GameNight>().OwnsOne(g => g.Address).HasData(
        //     new { Id = 1, GameNightId = 1, City = "Breda", Street = "Lovensdijkstraat", HouseNumber = 61 }
        // );
        //
        // modelBuilder.Entity<Game>().HasData(games);
        //
        // modelBuilder.Entity<GameNight>().HasData(gameNights);
        //
        // modelBuilder.Entity<User>().HasData(users); 
    }
}