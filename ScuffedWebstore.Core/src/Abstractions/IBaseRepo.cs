using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IBaseRepo<T> where T : BaseEntity
{
    public IEnumerable<T> GetAll(GetAllParams options);
    public T? GetOneById(Guid id);
    public T CreateOne(T createObject);
    public T UpdateOne(T updateObject);
    public bool DeleteOne(Guid id);
}