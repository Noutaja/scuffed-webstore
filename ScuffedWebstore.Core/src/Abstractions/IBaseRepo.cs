using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IBaseRepo<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync(GetAllParams options);
    public Task<T?> GetOneByIdAsync(Guid id);
    public Task<T> CreateOneAsync(T createObject);
    public Task<T> UpdateOneAsync(T updateObject);
    public Task<bool> DeleteOneAsync(Guid id);
}