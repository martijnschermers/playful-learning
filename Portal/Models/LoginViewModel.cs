using System.ComponentModel.DataAnnotations;

namespace Portal.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
}