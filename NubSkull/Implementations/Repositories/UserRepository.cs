using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.Identity;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUser(User User)
    {
         await _context.Users.AddAsync(User);
        await _context.SaveChangesAsync();
        return User;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
       return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.EmailAddress == email);
        return user;
    }

    public async Task<User> GetUserById(int Id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == Id);
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
