using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class ImageRepo : BaseRepo<Image>, IImageRepo
{
    public ImageRepo(DatabaseContext database) : base(database)
    {
    }

    public override async Task<IEnumerable<Image>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public async Task<IEnumerable<Image>> UpdateProductImages(IEnumerable<Image> updates, IEnumerable<Image> creates, IEnumerable<Image> deletes)
    {
        using (var transaction = await _database.Database.BeginTransactionAsync())
        {
            try
            {
                Guid? productID = null;
                foreach (Image img in deletes)
                {
                    int i = updates.ToList().FindIndex((img) => img.ID == img.ID);
                    if (i >= 0) updates.ToList().RemoveAt(i);

                    if (img != null) productID = img.ProductID;

                    Console.WriteLine(img.ID);
                    _data.Remove(img);
                }

                foreach (Image img in creates)
                {
                    if (img != null) productID = img.ProductID;

                    _data.Add(img);
                }

                foreach (Image img in updates)
                {
                    if (img != null) productID = img.ProductID;

                    _data.Update(img);
                }
                await _database.SaveChangesAsync();
                await transaction.CommitAsync();

                if (productID == null) return new List<Image>();
                else return await _data.Where((i) => i.ProductID == productID).ToListAsync();
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