using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Portal.Models;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Wachtwoorden komen niet overeen!")]
    public string RepeatPassword { get; set; }
}