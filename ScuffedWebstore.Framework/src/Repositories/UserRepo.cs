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

    public override async Task<IEnumerable<User>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Include(u => u.Addresses)
            .Where(u => (u.FirstName + " " + u.LastName).Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();
    }

    public override async Task<User?> GetOneByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().Include(u => u.Addresses).FirstOrDefaultAsync(t => t.ID == id);
    }

    public async Task<User?> GetOneByEmailAsync(string email)
    {
        return await _data.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
}