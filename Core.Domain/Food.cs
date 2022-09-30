namespace Core.Domain;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsVegetarian { get; set; }
    public List<Allergy> Allergies { get; set; }
}