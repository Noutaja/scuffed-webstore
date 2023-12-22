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

    public virtual async Task<T> CreateOneAsync(T createObject)
    {
        _data.Add(createObject);
        await _database.SaveChangesAsync();
        return createObject;
    }

    public virtual async Task<bool> DeleteOneAsync(Guid id)
    {
        T? t = await GetOneByIdAsync(id);
        if (t == null) return false;

        _data.Remove(t);
        await _database.SaveChangesAsync();
        return true;
    }

    public abstract Task<IEnumerable<T>> GetAllAsync(GetAllParams options);

    public virtual async Task<T?> GetOneByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().FirstOrDefaultAsync(t => t.ID == id);
    }

    public virtual async Task<T> UpdateOneAsync(T updateObject)
    {
        _data.Update(updateObject);
        await _database.SaveChangesAsync();
        return updateObject;
    }
}