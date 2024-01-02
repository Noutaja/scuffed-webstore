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

    public override async Task<IEnumerable<Product>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images)
            .Where(p => p.Title.Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public override async Task<Product?> GetOneByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).FirstOrDefaultAsync(t => t.ID == id);
    }
}