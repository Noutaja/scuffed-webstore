using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class AuthService : IAuthService
{
    private IUserRepo _userRepo;
    private IAddressRepo _addressRepo;
    private ITokenService _tokenService;
    private IMapper _mapper;

    public AuthService(IUserRepo userRepo, IAddressRepo addressRepo, ITokenService tokenService, IMapper mapper)
    {
        _userRepo = userRepo;
        _addressRepo = addressRepo;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public string Login(string email, string password)
    {
        User? u = _userRepo.GetOneByEmail(email);
        if (u == null) throw CustomException.NotFoundException("User not found");

        if (PasswordHandler.VerifyPassword(password, u.Password, u.Salt)) return _tokenService.GenerateToken(u);

        throw CustomException.InvalidPassword();
    }

    public UserReadDTO GetProfile(Guid id)
    {
        return _mapper.Map<User?, UserReadDTO>(_userRepo.GetOneById(id));
    }

    public bool ChangePassword(Guid id, string newPassword)
    {
        User? u = _userRepo.GetOneById(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        var encrypted = PasswordHandler.HashPassword(newPassword);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;

        _userRepo.UpdateOne(u);
        return true;
    }

    public bool DeleteProfile(Guid id)
    {
        User? u = _userRepo.GetOneById(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        return _userRepo.DeleteOne(id);
    }
}