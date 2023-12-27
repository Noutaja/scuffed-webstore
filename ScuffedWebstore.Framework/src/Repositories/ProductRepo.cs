using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    public ProductRepo(DatabaseContext database) : base(database)
    {

    }

    public async Task<IEnumerable<Product>> GetAllAsync(GetAllProductsParams options)
    {
        return await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images)
            .Where(p => p.Title.Contains(options.Query)).Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public override async Task<Product?> GetOneByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).FirstOrDefaultAsync(t => t.ID == id);
    }

    /* public override async Task<Product> CreateOneAsync(Product createObject)
    {
        _data.Add(createObject);
        await _database.SaveChangesAsync();

        return await GetOneByIdAsync(createObject.ID);
    } */
}