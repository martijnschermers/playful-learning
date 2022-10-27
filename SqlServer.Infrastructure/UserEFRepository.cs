using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class UserEFRepository : IUserRepository
{
    private readonly DomainDbContext _context; 
    
    public UserEFRepository(DomainDbContext context, IDbContextFactory<DomainDbContext>? contextFactory)
    {
        _context = context; 
        if (contextFactory != null) _context = contextFactory.CreateDbContext();
    }
    
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges(); 
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users
            .Where(u => u.Email == email)
            .Include(u => u.Allergies)
            .Include(u => u.GameNights)
            .FirstOrDefault(); 
    }

    public User? GetUserById(int id)
    {
        return _context.Users
            .Where(u => u.Id == id)
            .Include(u => u.Allergies)
            .Include(u => u.Address)
            .FirstOrDefault();
    }

    public ICollection<User> GetAllUsers()
    {
        return _context.Users
            .Include(u => u.Allergies)
            .ToList();
    }
}