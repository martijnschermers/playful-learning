using Core.Domain;

namespace Core.DomainServices;

public interface IAllergyRepository
{
    IEnumerable<Allergy> GetAllAllergies();
    Allergy GetAllergyById(int id); 
}