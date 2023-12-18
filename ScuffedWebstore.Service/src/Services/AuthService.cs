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
    private ITokenService _tokenService;
    private IMapper _mapper;

    public AuthService(IUserRepo userRepo, ITokenService tokenService, IMapper mapper)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
        _mapper = mapper;
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
        return _mapper.Map<User?, UserReadDTO>(_userRepo.GetOneById(g));
    }
}