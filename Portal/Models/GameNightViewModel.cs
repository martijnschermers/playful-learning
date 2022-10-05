using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Portal.Models;

public class GameNightViewModel
{
    [Required]
    public DateTime DateTime { get; set; }
    [Required]
    public int MaxPlayers { get; set; }
    [Required]
    public bool IsOnlyForAdults { get; set; }
    [Required]
    public bool IsPotluck { get; set; }
    [Required]
    public int HouseNumber { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public List<Game> Games { get; set; } = new();
}