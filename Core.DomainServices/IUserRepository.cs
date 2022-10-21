using Core.Domain;

namespace Core.DomainServices;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetUserByEmail(string email); 
}