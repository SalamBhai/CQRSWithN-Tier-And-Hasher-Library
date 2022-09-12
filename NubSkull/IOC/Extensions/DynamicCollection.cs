namespace NubSkull.IOC;

public static class DynamicCollection
{
    public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories()
        //.AddCommands()
        //.AddQueries()
        .AddAuthentication();
        return services;
    }
}
