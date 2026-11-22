using FoundationsBusiness.Models;

namespace FoundationsBusiness.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetBySeasonIdAsync(int seasonId, CancellationToken cancellationToken = default);
}
