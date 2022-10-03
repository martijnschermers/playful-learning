using Core.Domain;
using Core.DomainServices;

namespace SqlServer.Infrastructure;

public class UserEFRepository : IUserRepository
{
    private readonly DomainDbContext _context; 
    
    public UserEFRepository(DomainDbContext context)
    {
        _context = context; 
    }
    
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges(); 
    }
}