using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;
using ScuffedWebstore.Service.src.Shared;

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
        User? u = GetOneById(id);
        if (u == null) return false;

        _users.Remove(u);
        _database.SaveChanges();
        return true;
    }

    public IEnumerable<User> GetAll(GetAllParams options)
    {
        return _users.Where(u => (u.FirstName + " " + u.LastName).Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
    }

    public User? GetOneById(Guid id)
    {
        return _users.FirstOrDefault(u => u.ID == id);
    }

    public User? GetOneByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public User UpdateOne(User user)
    {
        User? u = GetOneById(user.ID);
        if (u == null) throw CustomException.NotFoundException("User not found");

        _users.Update(user);
        _database.SaveChanges();
        return u;
    }
}