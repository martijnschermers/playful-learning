using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlServer.Infrastructure;

public class DomainDbContext : DbContext
{
    public DbSet<GameNight> GameNights { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
        
    }
}