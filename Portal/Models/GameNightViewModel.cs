using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Portal.Models;

public class GameNightViewModel
{
    [Required(ErrorMessage = "Datum en tijd is verplicht!")]
    public DateTime DateTime { get; set; }
    
    [Required(ErrorMessage = "Maximaal aantal spelers is verplicht!")]
    public int MaxPlayers { get; set; }
    
    public bool IsOnlyForAdults { get; set; }
    public bool IsPotluck { get; set; }
    
    [Required(ErrorMessage = "Huisnummer is verplicht!")]
    public int HouseNumber { get; set; }
    
    [Required(ErrorMessage = "Stad is verplicht!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Straat is verplicht!")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "Spellen zijn verplicht!")]
    public ICollection<Game> Games { get; set; } = new List<Game>();
}