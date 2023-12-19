using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Framework.src.Repositories;
public abstract class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
{
    protected readonly DbSet<T> _data;
    protected readonly DatabaseContext _database;

    public BaseRepo(DatabaseContext database)
    {
        _database = database;
        _data = _database.Set<T>();
    }

    public virtual T CreateOne(T createObject)
    {
        _data.Add(createObject);
        _database.SaveChanges();
        return createObject;
    }

    public virtual bool DeleteOne(Guid id)
    {
        T? t = GetOneById(id);
        if (t == null) return false;

        _data.Remove(t);
        _database.SaveChanges();
        return true;
    }

    public abstract IEnumerable<T> GetAll(GetAllParams options);

    public virtual T? GetOneById(Guid id)
    {
        return _data.AsNoTracking().FirstOrDefault(t => t.ID == id);
    }

    public virtual T UpdateOne(T updateObject)
    {
        _data.Update(updateObject);
        _database.SaveChanges();
        return updateObject;
    }
}