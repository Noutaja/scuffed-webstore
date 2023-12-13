using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions
{
    public interface IBaseRepo<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(GetAllParams options);
        T? GetOneById(Guid id);
        T CreateOne(T user);
        T UpdateOne(T user);
        bool DeleteOne(Guid id);
    }
}