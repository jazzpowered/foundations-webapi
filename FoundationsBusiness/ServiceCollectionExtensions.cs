using FoundationsBusiness.Repositories;
using FoundationsBusiness.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoundationsBusiness;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundationsBusiness(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));
        services.AddScoped<UserRepository>();
        services.AddScoped<ChurchRepository>();
        services.AddScoped<CountryRepository>();
        services.AddScoped<UserService>();

        return services;
    }
}
