using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Core.DomainServices.Services.Interface;

namespace Core.DomainServices.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public User? GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email); 
    }
}