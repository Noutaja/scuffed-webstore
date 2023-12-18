using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
{
    public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
    { }

    public override UserReadDTO CreateOne(UserCreateDTO user)
    {
        var encrypted = PasswordHandler.HashPassword(user.Password);
        User u = _mapper.Map<UserCreateDTO, User>(user);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;
        return _mapper.Map<User, UserReadDTO>(_repo.CreateOne(u));
    }
}