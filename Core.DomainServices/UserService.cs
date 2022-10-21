using Core.Domain;

namespace Core.DomainServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public User GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email); 
    }
}