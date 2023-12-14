using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Service.src.DTOs;
public class UserReadDTO
{
    public Guid ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public UserRole Role { get; set; }

    public UserReadDTO Convert(User user)
    {
        return new UserReadDTO
        {
            ID = user.ID,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Avatar = user.Avatar,
            Role = user.Role
        };
    }
}

public class UserCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public string Password { get; set; }

    public User Transform()
    {
        return new User
        {
            ID = new Guid(),
            FirstName = this.FirstName,
            LastName = this.LastName,
            Email = this.Email,
            Avatar = this.Avatar,
            Role = UserRole.Normal
        };
    }
}

public class UserUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }

    public User ApplyTo(User user)
    {
        if (!string.IsNullOrEmpty(this.FirstName)) user.FirstName = this.FirstName;
        if (!string.IsNullOrEmpty(this.LastName)) user.LastName = this.LastName;
        if (!string.IsNullOrEmpty(this.Email)) user.Email = this.Email;
        if (!string.IsNullOrEmpty(this.Avatar)) user.Avatar = this.Avatar;
        return user;
    }
}