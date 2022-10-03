using Core.Domain;

namespace Portal.Models;

public class RegisterViewModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}