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

    public override async Task<IEnumerable<Order>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Include(o => o.OrderProducts).ThenInclude(o => o.Product).ThenInclude(o => o.Category)
        .Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public override async Task<Order?> GetOneByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().Include(o => o.OrderProducts).ThenInclude(o => o.Product).ThenInclude(o => o.Category)
        .FirstOrDefaultAsync(t => t.ID == id);
    }

    public override async Task<Order> CreateOneAsync(Order createObject)
    {
        using (var transaction = await _database.Database.BeginTransactionAsync())
        {
            try
            {
                foreach (OrderProduct op in createObject.OrderProducts)
                {
                    Product product = await _products.FirstOrDefaultAsync(p => p.ID == op.ProductID)!;
                    product.Inventory -= op.Amount;
                    op.Price = product.Price;
                    _products.Update(product);
                    await _database.SaveChangesAsync();
                }
                Console.WriteLine(createObject.UserID);
                _data.Add(createObject);
                await _database.SaveChangesAsync();
                await transaction.CommitAsync();
                return await GetOneByIdAsync(createObject.ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}