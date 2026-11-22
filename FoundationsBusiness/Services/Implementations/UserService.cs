using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class UserService(IUserRepository userRepository)
    : Service<User>(userRepository), IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task<IEnumerable<User>> GetBySeasonIdAsync(
        int seasonId, CancellationToken cancellationToken = default) =>
        _userRepository.GetBySeasonIdAsync(seasonId, cancellationToken);
}
