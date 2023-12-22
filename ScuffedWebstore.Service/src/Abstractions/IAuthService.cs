using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAuthService
{
    public Task<string> LoginAsync(string email, string password);
    public Task<UserReadDTO> GetProfileAsync(Guid id);
    public Task<bool> DeleteProfileAsync(Guid id);
    public Task<bool> ChangePasswordAsync(Guid id, string newPassword);
}