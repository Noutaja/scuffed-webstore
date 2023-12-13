using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class UserService : IUserService
{
    private IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public UserReadDTO CreateOne(UserCreateDTO user)
    {
        var encrypted = PasswordHandler.HashPassword(user.Password);
        User u = user.Transform();
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;
        return new UserReadDTO().Convert(_userRepo.CreateOne(u));
    }

    public bool DeleteOne(Guid id)
    {
        return _userRepo.DeleteOne(id);
    }

    public IEnumerable<UserReadDTO> GetAll(GetAllParams options)
    {
        return _userRepo.GetAll(options).Select(u => new UserReadDTO().Convert(u));
    }

    public UserReadDTO? GetOneById(Guid id)
    {
        User? u = _userRepo.GetOneById(id);
        if (u == null) return null;

        return new UserReadDTO().Convert(u);
    }

    public UserReadDTO UpdateOne(Guid id, UserUpdateDTO userUpdateDTO)
    {
        User? u = _userRepo.GetOneById(id);
        if (u == null) throw CustomException.NotFoundException();

        return new UserReadDTO().Convert(_userRepo.UpdateOne(userUpdateDTO.ApplyTo(u)));
    }
}