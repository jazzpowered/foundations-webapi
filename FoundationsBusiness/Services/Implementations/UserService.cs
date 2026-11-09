using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class UserService(IUserRepository userRepository)
    : Service<User>(userRepository), IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
}
