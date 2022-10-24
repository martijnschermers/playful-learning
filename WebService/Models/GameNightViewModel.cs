using System.ComponentModel.DataAnnotations;
using Core.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

#pragma warning disable CS8618

namespace WebService.Models;

public class GameNightViewModel
{
    [Required(ErrorMessage = "City is verplicht!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Street is verplicht!")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "HouseNumber is verplicht!")]
    public int HouseNumber { get; set; }
    
    [Required(ErrorMessage = "MaxPlayers is verplicht!")]
    public int MaxPlayers { get; set; }
    
    [Required(ErrorMessage = "DateTime is verplicht!")]
    public DateTime DateTime { get; set; }
    
    [Display(Name = "Potluck?")]
    public bool IsPotluck { get; set; }
    
    [Required(ErrorMessage = "isOnlyForAdults is verplicht!")]
    public bool IsOnlyForAdults { get; set; }

    [Required(ErrorMessage = "Games zijn verplicht!")]
    public List<Game> Games { get; set; }
    
    public int? OrganizerId { get; set; }
}