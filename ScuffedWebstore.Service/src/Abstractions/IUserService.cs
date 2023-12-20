using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IUserService : IBaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public UserReadDTO AddAddress(Guid userId, Guid addressId);
    public UserReadDTO RemoveAddress(Guid userId, Guid addressId);
    public UserReadDTO CreateUserAddress(AddressCreateFullDTO addressCreateDto);
    public UserReadDTO UpdateRole(Guid id, UserRole role);
}