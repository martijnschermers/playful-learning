using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Portal.Models;

public class GameNightViewModel
{
    public GameNightViewModel()
    {
        Games = new List<Game>();
    }

    public GameNightViewModel(ICollection<Game> games)
    {
        Games = games; 
    }
    
    [Display(Name = "Adres: ")]
    [Required(ErrorMessage = "Stad is verplicht!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Straat is verplicht!")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "Huisnummer is verplicht!")]
    public int HouseNumber { get; set; }
    
    [Display(Name= "Maximaal aantal spelers: ")]
    [Required(ErrorMessage = "Maximaal aantal spelers is verplicht!")]
    public int MaxPlayers { get; set; }
    
    [Display(Name = "Datum en tijd: ")]
    [Required(ErrorMessage = "Datum en tijd is verplicht!")]
    public DateTime DateTime { get; set; }
    
    [Display(Name = "Potluck?")]
    public bool IsPotluck { get; set; }
    
    [Display(Name = "Alleen voor volwassenen?")]
    public bool IsOnlyForAdults { get; set; }

    [Required(ErrorMessage = "Spellen zijn verplicht!")]
    [Display(Name = "Spellen")]
    public ICollection<Game> Games { get; set; }
}