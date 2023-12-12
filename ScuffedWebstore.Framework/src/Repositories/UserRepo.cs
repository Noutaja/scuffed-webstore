using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class UserRepo : IUserRepo
{
    private DbSet<User> _users;
    private DatabaseContext _database;
    private IConfiguration _config;

    public UserRepo(DatabaseContext database, IConfiguration config)
    {
        _users = database.Users;
        _database = database;
        _config = config;
    }

    public User CreateOne(User user)
    {
        _users.Add(user);
        _database.SaveChanges();
        return user;
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll(GetAllUsersParams options)
    {
        return _users.Where(u => u.FirstName.Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
    }

    public User? GetOneById(Guid id)
    {
        return _users.FirstOrDefault(u => u.ID == id);
    }

    public User UpdateOne(User user)
    {
        throw new NotImplementedException();
    }
}