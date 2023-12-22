using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
{

    public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
    {

    }

    public override UserReadDTO CreateOne(Guid id, UserCreateDTO user)
    {
        var encrypted = PasswordHandler.HashPassword(user.Password);
        User u = _mapper.Map<UserCreateDTO, User>(user);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;
        u.ID = id;
        return _mapper.Map<User, UserReadDTO>(_repo.CreateOne(u));
    }


    public UserReadDTO UpdateRole(Guid id, UserRole role)
    {
        User? u = _repo.GetOneById(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        u.Role = role;

        return _mapper.Map<User, UserReadDTO>(_repo.UpdateOne(u));

    }
}