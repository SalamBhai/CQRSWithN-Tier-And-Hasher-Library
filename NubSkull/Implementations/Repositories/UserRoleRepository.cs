using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.Identity;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationContext _context;

        public UserRoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserRole> AddUserRoleAsync(UserRole userRole)
        {
           await _context.UserRoles.AddAsync(userRole);
           await _context.SaveChangesAsync();
           return userRole;
        }

        public async Task<List<UserRole>> GetAllUserRolesByUserId(int Id)
        {
            var userRoles =  await _context.UserRoles.Include( uR  => uR.User).
            Include(uR => uR.Role).
            Where(userRole => userRole.UserId == Id).ToListAsync();
            return userRoles;
        }
      public async Task<List<UserRole>> GetAllUserRoles()
        {
            var userRoles =  await _context.UserRoles.Include( uR  => uR.User).
            Include(uR => uR.Role).ToListAsync();
            return userRoles;
        }
    }
}