using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public override IEnumerable<Image> GetAll(GetAllParams options)
    {
        return _data.Skip(options.Offset).Take(options.Limit);
    }
}