using Core.Domain;

namespace Core.DomainServices.Repositories.Interface;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetUserByEmail(string email);
    User? GetUserById(int id);
    ICollection<User> GetAllUsers();
}