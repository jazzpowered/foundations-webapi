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
        services.AddScoped<RoleRepository>();
        services.AddScoped<WeekSessionRepository>();
        services.AddScoped<LanguageRepository>();
        services.AddScoped<SemesterRepository>();
        services.AddScoped<SeasonRepository>();
        services.AddScoped<ParticipantTableRepository>();
        services.AddScoped<AttendanceRepository>();
        services.AddScoped<IRepository<User>>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IRepository<Church>>(provider => provider.GetRequiredService<ChurchRepository>());
        services.AddScoped<IRepository<Country>>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<IRepository<Role>>(provider => provider.GetRequiredService<RoleRepository>());
        services.AddScoped<IRepository<WeekSession>>(provider => provider.GetRequiredService<WeekSessionRepository>());
        services.AddScoped<IRepository<Language>>(provider => provider.GetRequiredService<LanguageRepository>());
        services.AddScoped<IRepository<Semester>>(provider => provider.GetRequiredService<SemesterRepository>());
        services.AddScoped<IRepository<Season>>(provider => provider.GetRequiredService<SeasonRepository>());
        services.AddScoped<IRepository<ParticipantTable>>(provider => provider.GetRequiredService<ParticipantTableRepository>());
        services.AddScoped<IRepository<Attendance>>(provider => provider.GetRequiredService<AttendanceRepository>());
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IChurchRepository>(provider => provider.GetRequiredService<ChurchRepository>());
        services.AddScoped<ICountryRepository>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<IRoleRepository>(provider => provider.GetRequiredService<RoleRepository>());
        services.AddScoped<IWeekSessionRepository>(provider => provider.GetRequiredService<WeekSessionRepository>());
        services.AddScoped<ILanguageRepository>(provider => provider.GetRequiredService<LanguageRepository>());
        services.AddScoped<ISemesterRepository>(provider => provider.GetRequiredService<SemesterRepository>());
        services.AddScoped<ISeasonRepository>(provider => provider.GetRequiredService<SeasonRepository>());
        services.AddScoped<IParticipantTableRepository>(provider => provider.GetRequiredService<ParticipantTableRepository>());
        services.AddScoped<IAttendanceRepository>(provider => provider.GetRequiredService<AttendanceRepository>());
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IWeekSessionService, WeekSessionService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<ISemesterService, SemesterService>();
        services.AddScoped<ISeasonService, SeasonService>();
        services.AddScoped<IParticipantTableService, ParticipantTableService>();
        services.AddScoped<IAttendanceService, AttendanceService>();

        return services;
    }
}
