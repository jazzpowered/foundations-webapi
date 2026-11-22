using FoundationsBusiness.Models;

namespace FoundationsBusiness.Services.Interfaces;

public interface IUserService : IService<User>
{
    Task<IEnumerable<User>> GetBySeasonIdAsync(int seasonId, CancellationToken cancellationToken = default);
}
