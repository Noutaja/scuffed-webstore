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

    public async Task<string> LoginAsync(string email, string password)
    {
        User? u = await _userRepo.GetOneByEmailAsync(email);
        if (u == null) throw CustomException.InvalidPasswordOrEmail();

        if (PasswordHandler.VerifyPassword(password, u.Password, u.Salt)) return _tokenService.GenerateToken(u);

        throw CustomException.InvalidPasswordOrEmail();
    }

    public async Task<UserReadDTO> GetProfileAsync(Guid id)
    {
        return _mapper.Map<User?, UserReadDTO>(await _userRepo.GetOneByIdAsync(id));
    }

    public async Task<bool> ChangePasswordAsync(Guid id, string newPassword)
    {
        User? u = await _userRepo.GetOneByIdAsync(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        var encrypted = PasswordHandler.HashPassword(newPassword);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;

        await _userRepo.UpdateOneAsync(u);
        return true;
    }

    public async Task<bool> DeleteProfileAsync(Guid id)
    {
        return await _userRepo.DeleteOneAsync(id);
    }
}