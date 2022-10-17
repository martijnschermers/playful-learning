using Core.Domain;
using Core.DomainServices;

namespace SqlServer.Infrastructure;

public class AllergyEFRepository : IAllergyRepository
{
    private readonly DomainDbContext _context; 
    
    public AllergyEFRepository(DomainDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Allergy> GetAllAllergies()
    {
        return _context.Allergies.ToList();
    }

    public Allergy GetAllergyById(int id)
    {
        return _context.Allergies.First(a => a.Id == id);
    }
}