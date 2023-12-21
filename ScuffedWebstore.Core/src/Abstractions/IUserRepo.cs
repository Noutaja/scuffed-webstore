using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IUserRepo : IBaseRepo<User>
{
    public User? GetOneByEmail(string email);
    public IEnumerable<User> GetAll(GetAllUsersParams options);
}