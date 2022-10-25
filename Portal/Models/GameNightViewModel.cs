using System.ComponentModel.DataAnnotations;
using ApplicationServices;
using Core.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

#pragma warning disable CS8618

namespace Portal.Models;

public class GameNightViewModel
{
    [Display(Name = "Stad: ")]
    [Required(ErrorMessage = "Stad is verplicht!")]
    public string City { get; set; }
    
    [Display(Name = "Straat: ")]
    [Required(ErrorMessage = "Straat is verplicht!")]
    public string Street { get; set; }
    
    [Display(Name = "Huisnummer: ")]
    [Required(ErrorMessage = "Huisnummer is verplicht!")]
    [Range(1, 1000, ErrorMessage = "Groter dan nul!")]
    public int HouseNumber { get; set; }
    
    [Display(Name= "Maximaal aantal spelers: ")]
    [Required(ErrorMessage = "Maximaal aantal spelers is verplicht!")]
    [Range(1, 1000, ErrorMessage = "Getal moet groter dan nul zijn!")]
    public int MaxPlayers { get; set; }
    
    [Display(Name = "Datum en tijd: ")]
    [Required(ErrorMessage = "Datum en tijd is verplicht!")]
    [FutureDateTime(ErrorMessage = "Datum mag niet in het verleden liggen!")]
    public DateTime DateTime { get; set; }
    
    [Display(Name = "Potluck?")]
    public bool IsPotluck { get; set; }
    
    [Display(Name = "Alleen voor volwassenen?")]
    public bool IsOnlyForAdults { get; set; }

    [Display(Name = "Spellen: ")]
    [ValidateNever]
    public List<CheckboxOption<Game>> Games { get; set; }
    
    [Required(ErrorMessage = "Spellen zijn verplicht!")]
    public List<int> Game { get; set; }
}