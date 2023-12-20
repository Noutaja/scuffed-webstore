using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
{
    private IAddressRepo _addressRepo;

    public UserService(IUserRepo userRepo, IAddressRepo addressRepo, IMapper mapper) : base(userRepo, mapper)
    {
        _addressRepo = addressRepo;
    }

    public override UserReadDTO CreateOne(UserCreateDTO user)
    {
        var encrypted = PasswordHandler.HashPassword(user.Password);
        User u = _mapper.Map<UserCreateDTO, User>(user);
        u.Password = encrypted.password;
        u.Salt = encrypted.salt;
        return _mapper.Map<User, UserReadDTO>(_repo.CreateOne(u));
    }

    public UserReadDTO UpdateRole(Guid id, UserRole role)
    {
        User? u = _repo.GetOneById(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        u.Role = role;

        return _mapper.Map<User, UserReadDTO>(_repo.UpdateOne(u));
    }

    public UserReadDTO AddAddress(Guid userId, Guid addressId)
    {
        User? u = _repo.GetOneById(userId);
        if (u == null) throw CustomException.NotFoundException("User not found");

        Address? a = _addressRepo.GetOneById(addressId);
        if (a == null) throw CustomException.NotFoundException("Address not found");

        u.Addresses = u.Addresses.Append(a);

        return _mapper.Map<User, UserReadDTO>(_repo.UpdateOne(u));
    }

    public UserReadDTO RemoveAddress(Guid userId, Guid addressId)
    {
        User? u = _repo.GetOneById(userId);
        if (u == null) throw CustomException.NotFoundException("User not found");

        Address? a = _addressRepo.GetOneById(addressId);
        if (a == null) throw CustomException.NotFoundException("Address not found");

        u.Addresses = u.Addresses.Where(a => a.ID == addressId);

        return _mapper.Map<User, UserReadDTO>(_repo.UpdateOne(u));
    }

    public UserReadDTO CreateUserAddress(AddressCreateDTO addressCreateDto)
    {
        User? u = _repo.GetOneById(addressCreateDto.UserID);
        if (u == null) throw CustomException.NotFoundException("User not found");

        Address a = _addressRepo.CreateOne(_mapper.Map<AddressCreateDTO, Address>(addressCreateDto));
        return AddAddress(a.UserID, a.ID);
    }
}