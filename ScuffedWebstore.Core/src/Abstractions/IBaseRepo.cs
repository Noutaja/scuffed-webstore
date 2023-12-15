using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IBaseRepo<T> where T : BaseEntity
{
    IEnumerable<T> GetAll(GetAllParams options);
    T? GetOneById(Guid id);
    T CreateOne(T createObject);
    T UpdateOne(T updateObject);
    bool DeleteOne(Guid id);
}