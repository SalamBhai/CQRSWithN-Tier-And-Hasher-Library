using NubSkull.Identity;

namespace NubSkull.Interfaces.IRepositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetSelectedRolesAsync(List<string> RoleNames);
    Task<Role> GetRoleByName(string RoleName);
    Task<Role> GetRoleById(int Id);
    Task<IEnumerable<Role>> GetAllRoles();
    Task<Role> AddRoleAsync(Role role);
    Task<Role> UpdateRoleAsync(Role role);
}
