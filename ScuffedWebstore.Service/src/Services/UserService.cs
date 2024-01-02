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

    public override async Task<UserReadDTO> CreateOneAsync(Guid id, UserCreateDTO createObject)
    {
        if (createObject.FirstName.Length < 1)
        {
            throw CustomException.InvalidParameters("FirstName can't be less than 1 characters long");
        }
        if (createObject.LastName.Length < 1)
        {
            throw CustomException.InvalidParameters("LastName can't be less than 1 characters long");
        }
        if (createObject.Password.Length < 8)
        {
            throw CustomException.InvalidParameters("Password can't be less than 3 characters long");
        }
        if (!createObject.Password.Any(char.IsDigit))
        {
            throw CustomException.InvalidParameters("Password must have a number");
        }
        if (!Uri.IsWellFormedUriString(createObject.Avatar, UriKind.Absolute))
        {
            throw CustomException.InvalidParameters("Not a valid URI");
        }
        var encrypted = PasswordHandler.HashPassword(createObject.Password);
        User u = _mapper.Map<UserCreateDTO, User>(createObject);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;
        u.ID = id;
        return _mapper.Map<User, UserReadDTO>(await _repo.CreateOneAsync(u));
    }


    public async Task<UserReadDTO> UpdateRoleAsync(Guid id, UserRole role)
    {
        User? u = await _repo.GetOneByIdAsync(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        u.Role = role;

        return _mapper.Map<User, UserReadDTO>(await _repo.UpdateOneAsync(u));

    }

    public override Task<UserReadDTO> UpdateOneAsync(Guid id, UserUpdateDTO updateObject)
    {
        if (updateObject.FirstName != null && updateObject.FirstName.Length < 1)
        {
            throw CustomException.InvalidParameters("FirstName can't be less than 1 characters long");
        }
        if (updateObject.LastName != null && updateObject.LastName.Length < 1)
        {
            throw CustomException.InvalidParameters("LastName can't be less than 1 characters long");
        }
        if (updateObject.Avatar != null && !Uri.IsWellFormedUriString(updateObject.Avatar, UriKind.Absolute))
        {
            throw CustomException.InvalidParameters("Not a valid URI");
        }

        return base.UpdateOneAsync(id, updateObject);
    }
}