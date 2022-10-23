using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

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

    public User? GetUserByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email)
            .Include(u => u.Allergies)
            .FirstOrDefault(); 
    }
}