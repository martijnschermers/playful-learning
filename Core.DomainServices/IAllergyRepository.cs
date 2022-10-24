using Core.Domain;

namespace Core.DomainServices;

public interface IAllergyRepository
{
    IEnumerable<Allergy> GetAllAllergies();
    List<Allergy> GetAllergiesByIds(List<int> allergyIds);
    Allergy GetAllergyById(int id); 
}