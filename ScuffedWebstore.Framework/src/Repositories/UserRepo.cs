using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Framework.src.Repositories;
public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(DatabaseContext database) : base(database)
    { }

    public override IEnumerable<User> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Where(u => (u.FirstName + " " + u.LastName).Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
    }

    public User? GetOneByEmail(string email)
    {
        return _data.AsNoTracking().FirstOrDefault(u => u.Email == email);
    }
}