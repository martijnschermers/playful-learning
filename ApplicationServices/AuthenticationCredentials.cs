using System.ComponentModel.DataAnnotations;

namespace WebService.Models;

public class AuthenticationCredentials

{
    public AuthenticationCredentials(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
}