using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal abstract class Service<T>(IRepository<T> repository) : IService<T>
{
    public Task<IEnumerable<T>> GetAllAsync() => repository.GetAllAsync();

    public Task<T?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task<int> AddAsync(T entity) => repository.AddAsync(entity);

    public Task<bool> UpdateAsync(T entity) => repository.UpdateAsync(entity);

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);
}
