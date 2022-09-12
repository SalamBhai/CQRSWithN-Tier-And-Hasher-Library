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
        modelBuilder.Entity<User>().HasMany<UserRole>().WithOne(t => t.User).HasForeignKey(t => t.UserId);
        modelBuilder.Entity<User>().HasIndex(u => u.EmailAddress).IsUnique();
        modelBuilder.Entity<Role>().HasMany<UserRole>().WithOne(t => t.Role).HasForeignKey(t => t.RoleId);
    }

    public DbSet<User> Users{get;set;}
    public DbSet<UserRole> UserRoles{get;set;}
    public DbSet<Role> Roles{get;set;}
}
