using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Portal.Models;

public class GameNightViewModel
{
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

    [Display(Name = "Spellen")]
    [ValidateNever]
    public List<CheckboxOption> Games { get; set; }
    
    [Required(ErrorMessage = "Spellen zijn verplicht!")]
    public List<int> Game { get; set; }
}