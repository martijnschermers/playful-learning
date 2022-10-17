using Core.Domain;

namespace Core.DomainServices;

public interface IAllergyRepository
{
    ICollection<Allergy> GetAllAllergies();
    Allergy GetAllergyById(int id); 
}