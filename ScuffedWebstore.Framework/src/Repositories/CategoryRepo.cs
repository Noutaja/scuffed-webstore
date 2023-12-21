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

        public IEnumerable<Category> GetAll(GetAllCategoriesParams options)
        {
            return _data.AsNoTracking().Where(c => c.Name.Contains(options.Query)).Skip(options.Offset).Take(options.Limit);
        }

        public override IEnumerable<Category> GetAll(GetAllParams options)
        {
            return _data.AsNoTracking().Skip(options.Offset).Take(options.Limit);
        }
    }
}