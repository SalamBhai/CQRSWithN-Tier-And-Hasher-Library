using NubSkull.Authentication;
using NubSkull.Authentication.Hasher;
using NubSkull.Implementations.Commands;
using NubSkull.Implementations.Queries;
using NubSkull.Implementations.Repositories;
using NubSkull.Interfaces.ICommands;

using NubSkull.Interfaces.IRepositories;
namespace NubSkull.IOC;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        return services;
    }      
   
    // public static IServiceCollection AddCommands(this IServiceCollection services)
    // {
    //      services.AddScoped<ILoginCommand, LoginCommand>();
    //      services.AddScoped<IRegisterRoleCommand, RegisterRoleCommand>();
    //      services.AddScoped<IRegisterUserCommand, RegisterUserCommand>();
    //      services.AddScoped<IUpdateRoleCommand, UpdateRoleCommand>();
    //      services.AddScoped<IUpdateUserCommand, UpdateUserCommand>();
    //      return services;
    // }
      
    // public static IServiceCollection AddQueries(this IServiceCollection services)
    // {
    //     services.AddScoped<IGetAllRolesQuery, GetAllRolesQuery>();
    //     services.AddScoped<IGetAllUsersQuery, GetAllUsersQuery>();
    //     services.AddScoped<IGetRoleByNameQuery, GetRoleByNameQuery>();
    //     services.AddScoped<IGetSelectedRolesQuery, GetSelectedRolesQuery>();
    //     services.AddScoped<IGetUserByEmailQuery, GetUserByEmailQuery>();
    //     return services;
    // }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddScoped<IHasher, Hasher>();
        return services;
    }
  

}
