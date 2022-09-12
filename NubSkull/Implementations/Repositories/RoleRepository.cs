using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.Identity;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Repositories;

public class RoleRepository : IRoleRepository
{
     private readonly ApplicationContext _context;

    public RoleRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Role> AddRoleAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
         return role;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var allRoles = await _context.Roles.ToListAsync();
        return allRoles;
    }

    public async Task<Role> GetRoleByName(string RoleName)
    {
       var selectedRole= await _context.Roles.
        Where(role => role.Name == RoleName).SingleOrDefaultAsync();
        return selectedRole;
    }

    public async Task<Role> GetRoleById(int Id)
    {
       var selectedRole = await _context.Roles.
        Where(role => role.Id == Id).SingleOrDefaultAsync();
        return selectedRole;
    }


    public async Task<IEnumerable<Role>> GetSelectedRolesAsync(List<string> RoleNames)
    {
        var selectedRoles = await _context.Roles.
        Where(role => RoleNames.Contains(role.Name)).ToListAsync();
        return selectedRoles;
    }

    public async Task<Role> UpdateRoleAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }
}
