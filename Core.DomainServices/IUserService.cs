using Core.Domain;

namespace Core.DomainServices;

public interface IUserService
{
    User? GetUserByEmail(string email); 
}