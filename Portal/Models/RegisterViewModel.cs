using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Portal.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Gebruikersnaam is verplicht!")]
    public string Username { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Emailadres is verplicht!")]
    public string Email { get; set; }
    
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Stad is verplicht!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Straat is verplicht!")]
    public string Street { get; set; }
    
    public int HouseNumber { get; set; }

    public Gender Gender { get; set; }
    
    public UserType UserType { get; set; }
    
    // public List<Allergy> Allergies { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Wachtwoord is verplicht!")]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Wachtwoorden komen niet overeen!")]
    [Required(ErrorMessage = "Wachtwoord herhalen is verplicht!")]
    public string RepeatPassword { get; set; }
}