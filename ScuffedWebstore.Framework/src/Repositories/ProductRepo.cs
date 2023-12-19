using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    public ProductRepo(DatabaseContext database) : base(database)
    { }

    public override IEnumerable<Product> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Where(p => p.Title.Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
    }
}