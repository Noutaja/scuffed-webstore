using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAuthService
{
    public string Login(string email, string password);
    public UserReadDTO GetProfile(Guid id);
    public bool DeleteProfile(Guid id);
    public bool ChangePassword(Guid id, string newPassword);
}