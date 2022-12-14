namespace Core.Domain;

// ReSharper disable UnusedAutoPropertyAccessor.Global

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    private DateTime _birthDate;

    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (value > DateTime.Now) {
                throw new InvalidOperationException("De geboortedatum mag niet in de toekomst liggen!"); 
            }

            if (GetAge(value) < 16) {
                throw new InvalidOperationException("Je moet minimaal 16 jaar oud zijn om een account te registreren!"); 
            }

            if (Type == UserType.Organizer && GetAge(value) < 18) {
                throw new InvalidOperationException("Je moet 18 jaar oud zijn om een organisator te zijn!");
            }

            _birthDate = value; 
        }
    }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
    public UserType Type { get; set; }
    public ICollection<Allergy> Allergies { get; set; }
    public ICollection<GameNight> GameNights { get; set; }
    
    public int GetAge(DateTime? birthDate)
    {
        var age = DateTime.Now.Year - (birthDate ?? BirthDate).Year;
        if (BirthDate.Date > DateTime.Now.AddYears(-age)) age--;
        return age; 
    }
}