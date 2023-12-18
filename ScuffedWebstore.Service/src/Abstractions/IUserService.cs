using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IUserService : IBaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    /* public UserReadDTO CreateOne(UserCreateDTO user);
    public bool DeleteOne(Guid id);
    public IEnumerable<UserReadDTO> GetAll(GetAllParams options);
    public UserReadDTO? GetOneById(Guid id);
    public UserReadDTO UpdateOne(Guid id, UserUpdateDTO user); */
}