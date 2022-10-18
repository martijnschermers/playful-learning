namespace Core.Domain;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
    public UserType Type { get; set; }
    public bool IsVegetarian { get; set; }
    public ICollection<Allergy> Allergies { get; set; }

    public int GetAge()
    {
        var age = DateTime.Now.Year - BirthDate.Year;
        if (BirthDate.Date > DateTime.Now.AddYears(-age)) age--;
        return age; 
    }
}