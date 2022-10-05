namespace Core.Domain;

public class GameNight
{
    public int Id { get; set; }
    public int OrganizerId { get; set; }
    public User Organizer { get; set; }
    public DateTime DateTime { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsOnlyForAdults { get; set; }
    public bool IsPotluck { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public ICollection<Game> Games { get; set; }
    public ICollection<Drink> Drinks { get; set; }
    public ICollection<Food> Foods { get; set; }
    public ICollection<User> Players { get; set; }
}