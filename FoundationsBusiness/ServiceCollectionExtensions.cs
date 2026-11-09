using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Implementations;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Implementations;
using FoundationsBusiness.Services.Interfaces;
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
        services.AddScoped<IRepository<User>>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IRepository<Church>>(provider => provider.GetRequiredService<ChurchRepository>());
        services.AddScoped<IRepository<Country>>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IChurchRepository>(provider => provider.GetRequiredService<ChurchRepository>());
        services.AddScoped<ICountryRepository>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<ICountryService, CountryService>();

        return services;
    }
}
