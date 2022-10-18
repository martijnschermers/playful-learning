using System.ComponentModel.DataAnnotations;
using Core.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Portal.Models;

public class RegisterViewModel
{
    [Display(Name = "Gebruikersnaam: ")]
    [Required(ErrorMessage = "Gebruikersnaam is verplicht!")]
    public string Username { get; set; }
    
    [EmailAddress]
    [Display(Name = "Emailadres: ")]
    [Required(ErrorMessage = "Emailadres is verplicht!")]
    public string Email { get; set; }
    
    [Display(Name = "Geboortedatum: ")]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Adres: ")]
    [Required(ErrorMessage = "Stad is verplicht!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Straat is verplicht!")]
    public string Street { get; set; }
    
    public int HouseNumber { get; set; }

    [Display(Name = "Geslacht: ")]
    public Gender Gender { get; set; }
    
    [Display(Name = "Type gebruiker: ")]
    public UserType UserType { get; set; }
    [Display(Name = "Vegetariër: ")]
    public bool IsVegetarian { get; set; }
    
    [ValidateNever]
    [Display(Name = "Allergieën: ")]
    public ICollection<Allergy> Allergies { get; set; }
    
    [Display(Name = "Wachtwoord: ")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Wachtwoord is verplicht!")]
    public string Password { get; set; }
    
    [Display(Name = "Herhaal wachtwoord: ")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Wachtwoorden komen niet overeen!")]
    [Required(ErrorMessage = "Wachtwoord herhalen is verplicht!")]
    public string RepeatPassword { get; set; }
}