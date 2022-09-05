using Microsoft.EntityFrameworkCore;
using NubSkull.Identity;

namespace NubSkull.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<User> Users{get;set;}
    public DbSet<UserRole> UserRoles{get;set;}
    public DbSet<Role> Roles{get;set;}
}
