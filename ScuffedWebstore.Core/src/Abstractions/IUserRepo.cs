using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IUserRepo : IBaseRepo<User>
{
    public Task<User?> GetOneByEmailAsync(string email);
    public Task<IEnumerable<User>> GetAllAsync(GetAllUsersParams options);
}