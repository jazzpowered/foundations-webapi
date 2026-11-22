using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class UserRepository(IDbConnectionFactory connectionFactory)
    : Repository<User>(
        connectionFactory,
        "Users",
        "Id",
        "ChurchId",
        "CountryId",
        "RoleId",
        "FirstName",
        "LastName",
        "Email",
        "Gender",
        "Area",
        "ZipCode"),
        IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
}
