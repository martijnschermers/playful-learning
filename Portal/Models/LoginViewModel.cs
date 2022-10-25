using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

#pragma warning disable CS8618

namespace Portal.Models;

public class LoginViewModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Emailadres is verplicht!")]
    [Display(Name = "Emailadres: ")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Wachtwoord is verplicht!")]
    [Display(Name = "Wachtwoord: ")]
    public string Password { get; set; }
    
    [HiddenInput]
    [ValidateNever]
    public string ReturnUrl { get; set; } =  "/";
}