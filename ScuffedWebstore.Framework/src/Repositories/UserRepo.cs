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

    public IEnumerable<User> GetAll(GetAllUsersParams options)
    {
        return _data.AsNoTracking().Include(u => u.Addresses).Where(u => (u.FirstName + " " + u.LastName).Contains(options.Query)).Skip(options.Offset).Take(options.Limit);
    }

    public override IEnumerable<User> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Include(u => u.Addresses).Skip(options.Offset).Take(options.Limit);
    }

    public User? GetOneByEmail(string email)
    {
        return _data.AsNoTracking().FirstOrDefault(u => u.Email == email);
    }
}