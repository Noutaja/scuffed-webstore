using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAuthService
{
    public string Login(string email, string password);
    public UserReadDTO GetProfile(string id);
}