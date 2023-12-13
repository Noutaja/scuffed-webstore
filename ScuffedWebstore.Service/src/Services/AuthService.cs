using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class AuthService : IAuthService
{
    private IUserRepo _userRepo;

    public AuthService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public string Login(string email, string password)
    {
        User? u = _userRepo.GetOneByEmail(email);
        if (u == null) throw CustomException.NotFoundException("User not found");

        if (!PasswordHandler.VerifyPassword(password, u.Password, u.Salt)) return "token here";
        throw CustomException.InvalidPassword();
    }
}