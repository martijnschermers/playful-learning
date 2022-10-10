using Core.Domain;

namespace WebService.GraphQL;

public class UserQuery
{
    public User GetUser()
    {
        return new User
        {
            Address = new Address
            {
                City = "Dussen",
                Street = "Bredeweer",
                HouseNumber = 10
            },
            Email = "martijnschermers2@gmail.com",
            Gender = Gender.Male, Allergies = new List<Allergy> { new Allergy { Name = AllergyEnum.Gluten } },
            Name = "Martijn", Type = UserType.Organizer, BirthDate = DateTime.Now
        };
    }
}