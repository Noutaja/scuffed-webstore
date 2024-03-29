using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IUserService : IBaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public Task<UserReadDTO> UpdateRoleAsync(Guid id, UserRole role);
}