using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(DatabaseContext database) : base(database)
        { }

        public async Task<IEnumerable<Category>> GetAllAsync(GetAllCategoriesParams options)
        {
            return await _data.AsNoTracking().Where(c => c.Name.Contains(options.Query)).Skip(options.Offset).Take(options.Limit).ToListAsync();
        }

        public override async Task<IEnumerable<Category>> GetAllAsync(GetAllParams options)
        {
            return await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToListAsync();
        }
    }
}