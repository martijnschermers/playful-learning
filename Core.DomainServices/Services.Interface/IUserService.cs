using Core.Domain;

namespace Core.DomainServices.Services.Interface;

public interface IUserService
{
    User? GetUserByEmail(string email); 
}