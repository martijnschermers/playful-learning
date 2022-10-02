namespace Core.Domain;

public class Allergy
{
    public int Id { get; set; }
    public AllergyEnum Name { get; set; }
}

public enum AllergyEnum
{
    Nuts, 
    Lactose
}