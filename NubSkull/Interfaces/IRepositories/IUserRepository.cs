using NubSkull.DTOs.RequestModels;
using NubSkull.Identity;

namespace NubSkull.Interfaces.IRepositories;

public interface IUserRepository 
{
    Task<User> CreateUser(User User);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserByEmail(string email);
     Task<User> GetUserById(int Id);
    Task<User> UpdateUser(User user);
}
