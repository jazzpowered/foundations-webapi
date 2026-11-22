using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class SeasonService(ISeasonRepository seasonRepository)
    : Service<Season>(seasonRepository), ISeasonService
{
    private readonly ISeasonRepository _seasonRepository = seasonRepository;

    public Task<IEnumerable<Season>> GetByUserIdAsync(
        int userId, CancellationToken cancellationToken = default) =>
        _seasonRepository.GetByUserIdAsync(userId, cancellationToken);
}
