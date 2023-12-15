using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class AuthService : IAuthService
{
    private IUserRepo _userRepo;
    private ITokenService _tokenService;

    public AuthService(IUserRepo userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    public string Login(string email, string password)
    {
        User? u = _userRepo.GetOneByEmail(email);
        if (u == null) throw CustomException.NotFoundException("User not found");

        if (!PasswordHandler.VerifyPassword(password, u.Password, u.Salt)) return _tokenService.GenerateToken(u);

        throw CustomException.InvalidPassword();
    }

    public UserReadDTO GetProfile(string id)
    {
        Guid g = new Guid(id.ToString());
        return new UserReadDTO().Convert(_userRepo.GetOneById(g));
    }
}