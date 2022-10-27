namespace Core.Domain;

public class Game : ICheckboxOption
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsOnlyForAdults { get; set; }
    public string Image { get; set; }
    public GameType Type { get; set; }
    public Genre Genre { get; set; }
    public ICollection<GameNight> GameNights { get; set; }
}