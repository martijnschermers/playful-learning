using Microsoft.AspNetCore.Identity;

namespace Core.Domain;

public class GameNight
{
    public int Id { get; set; }
    public IdentityUser Organizer { get; set; }
    public DateTime DateTime { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsPotluck { get; set; }
    public Address Address { get; set; }
    public ICollection<Game> Games { get; set; }
    public ICollection<Drink> Drinks { get; set; }
    public ICollection<Food> Foods { get; set; }
    public ICollection<User> Players { get; set; }
}