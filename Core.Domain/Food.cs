namespace Core.Domain;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Allergy> Allergies { get; set; }
    public int? UserId { get; set; }
}