using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IUserRepo
{
    IEnumerable<User> GetAll(GetAllUsersParams options);
    User? GetOneById(Guid id);
    User CreateOne(User user);
    User UpdateOne(User user);
    bool DeleteOne(Guid id);
}