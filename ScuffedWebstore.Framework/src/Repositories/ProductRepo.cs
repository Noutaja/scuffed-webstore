using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    private DbSet<Image> _imageRepo;
    public ProductRepo(DatabaseContext database) : base(database)
    {
        _imageRepo = database.Images;
    }

    public IEnumerable<Product> GetAll(GetAllProductsParams options)
    {
        return _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).Where(p => p.Title.Contains(options.Query)).Skip(options.Offset).Take(options.Limit);
    }

    public override IEnumerable<Product> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).Skip(options.Offset).Take(options.Limit);
    }

    public override Product CreateOne(Product createObject)
    {
        foreach (Image i in createObject.Images)
        {
            i.ProductID = createObject.ID;
        }
        _data.Add(createObject);

        return createObject;
    }
}