using Core.Domain;

namespace Core.DomainServices.Services.Interface;

public interface IUserService
{
    User? GetUserById(int id); 
    User? GetUserByEmail(string email); 
}