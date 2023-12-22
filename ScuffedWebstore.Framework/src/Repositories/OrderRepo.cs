using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class OrderRepo : BaseRepo<Order>, IOrderRepo
{
    private DbSet<Product> _products;
    public OrderRepo(DatabaseContext database) : base(database)
    {
        _products = database.Products;
    }

    public override IEnumerable<Order> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Include(o => o.OrderProducts).Skip(options.Offset).Take(options.Limit);
    }

    public override Order CreateOne(Order createObject)
    {
        foreach (OrderProduct op in createObject.OrderProducts)
        {
            Console.WriteLine(op.Amount);
            Product product = _products.FirstOrDefault(p => p.ID == op.ProductID)!;
            product.Inventory -= op.Amount;
            op.Price = product.Price;
            _products.Update(product);
            _database.SaveChanges();
        }
        _data.Add(createObject);
        _database.SaveChanges();
        return createObject;

    }
}