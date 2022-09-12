using NubSkull.Identity;

namespace NubSkull.Interfaces.IRepositories
{
    public interface IUserRoleRepository
    {
        Task<UserRole> AddUserRoleAsync(UserRole userRole);
       Task<List<UserRole>> GetAllUserRolesByUserId(int Id);
       Task<List<UserRole>> GetAllUserRoles();
    }
}