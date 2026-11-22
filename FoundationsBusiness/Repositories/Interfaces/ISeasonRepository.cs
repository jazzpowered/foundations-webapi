using FoundationsBusiness.Models;

namespace FoundationsBusiness.Repositories.Interfaces;

public interface ISeasonRepository : IRepository<Season>
{
    Task<IEnumerable<Season>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
