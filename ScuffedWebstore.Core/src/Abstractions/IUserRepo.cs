using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IUserRepo : IBaseRepo<User>
{
    public User? GetOneByEmail(string email);
}