using Core.Domain;

namespace Portal.Models;

public class GameNightViewModel
{
    public User Organizer { get; set; }
    public DateTime DateTime { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsOnlyForAdults { get; set; }
    public bool IsPotluck { get; set; }
    
    public int HouseNumber { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    
    public List<Game> Games { get; set; } = new();
}