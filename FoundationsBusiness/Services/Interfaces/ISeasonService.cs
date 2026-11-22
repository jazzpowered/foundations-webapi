using FoundationsBusiness.Models;

namespace FoundationsBusiness.Services.Interfaces;

public interface ISeasonService : IService<Season>
{
    Task<IEnumerable<Season>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
