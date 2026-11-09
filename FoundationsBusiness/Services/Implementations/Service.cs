using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal abstract class Service<T>(IRepository<T> repository) : IService<T>
{
    private readonly IRepository<T> _repository = repository;

    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _repository.GetAllAsync(cancellationToken);

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _repository.GetByIdAsync(id, cancellationToken);

    public Task<int> AddAsync(T entity, CancellationToken cancellationToken = default) =>
        _repository.AddAsync(entity, cancellationToken);

    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
        _repository.UpdateAsync(entity, cancellationToken);

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _repository.DeleteAsync(id, cancellationToken);
}
