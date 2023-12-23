using AutoMapper;
using Moq;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;
using ScuffedWebstore.Service.src.Shared;
using Xunit;

namespace ScuffedWebstore.Test.src;
public class UserServiceTest
{
    public UserServiceTest()
    {

    }

    private static IMapper GetMapper()
    {
        MapperConfiguration mappingConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MapperProfile());
            });
        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }

    [Theory]
    [ClassData(typeof(GetAllUsersData))]
    public async void GetAll_ShouldReturnValidResponse(IEnumerable<User> response, IEnumerable<UserReadDTO> expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        UserService service = new UserService(repo.Object, GetMapper());

        IEnumerable<UserReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllUsersData : TheoryData<IEnumerable<User>, IEnumerable<UserReadDTO>>
    {
        public GetAllUsersData()
        {
            /* User user1 = new User() { FirstName = "Asd", LastName = "Asdeer", Email = "a@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            User user2 = new User() { FirstName = "Qwe", LastName = "Qwerty", Email = "q@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            User user3 = new User() { FirstName = "Zxc", LastName = "Zxcvbn", Email = "z@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" }; */
            IEnumerable<User> users = new List<User>();
            Add(users, GetMapper().Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturnValidResponse(User response, UserReadDTO expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        UserService service = new UserService(repo.Object, GetMapper());

        UserReadDTO result = await service.GetOneByIDAsync(It.IsAny<Guid>());

        Assert.Equivalent(expected, response);

    }

    public class GetOneByIDData : TheoryData<User, UserReadDTO>
    {
        public GetOneByIDData()
        {
            User user = new User()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Password = "asdf1234",
                Avatar = "https://picsum.photos/200",
                Addresses = new List<Address>()
            };
            Add(user, GetMapper().Map<User, UserReadDTO>(user));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneUserData))]
    public async void CreateOne_ShouldReturnValidResponse(UserCreateDTO input, User response, UserReadDTO expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<User>())).Returns(Task.FromResult(response));
        UserService service = new UserService(repo.Object, GetMapper());

        UserReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    public class CreateOneUserData : TheoryData<UserCreateDTO, User, UserReadDTO>
    {
        public CreateOneUserData()
        {
            UserCreateDTO userInput = new UserCreateDTO()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Password = "asdf1234",
                Avatar = "https://picsum.photos/200"
            };
            User user = GetMapper().Map<UserCreateDTO, User>(userInput);
            Add(userInput, user, GetMapper().Map<User, UserReadDTO>(user));
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        UserService service = new UserService(repo.Object, GetMapper());

        bool result = await service.DeleteOneAsync(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneUserData))]
    public async void UpdateOne_ShouldReturnValidResponse(UserUpdateDTO? input, User? foundUser, User? response, UserReadDTO? expected, Type? exception)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<User>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        UserService service = new UserService(repo.Object, GetMapper());

        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            UserReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneUserData : TheoryData<UserUpdateDTO?, User?, User?, UserReadDTO?, Type?>
    {
        public UpdateOneUserData()
        {
            UserUpdateDTO userInput = new UserUpdateDTO()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Avatar = "https://picsum.photos/200"
            };
            User user = new User()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Avatar = "https://picsum.photos/200"
            };
            UserUpdateDTO partialUserInput = new UserUpdateDTO()
            {
                FirstName = "Asd"
            };
            //User user = GetMapper().Map<UserUpdateDTO, User>(userInput);
            Add(userInput, user, user, GetMapper().Map<User, UserReadDTO>(user), null);
            Add(partialUserInput, user, user, GetMapper().Map<User, UserReadDTO>(user), null);
            Add(userInput, null, null, null, typeof(CustomException));
        }
    }

    [Theory]
    [ClassData(typeof(UpdateRoleData))]
    public async void UpdateRole_ShouldReturnValidResponse(UserRole input, User? foundUser, User? response, UserReadDTO? expected, Type? exception)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<User>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        UserService service = new UserService(repo.Object, GetMapper());

        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateRoleAsync(It.IsAny<Guid>(), input));
        else
        {
            UserReadDTO result = await service.UpdateRoleAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateRoleData : TheoryData<UserRole, User?, User?, UserReadDTO?, Type?>
    {
        public UpdateRoleData()
        {
            User user = new User()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Password = "asdf1234",
                Avatar = "https://picsum.photos/200",
                Role = UserRole.Normal,
                Addresses = new List<Address>()
            };
            User userExpected = new User()
            {
                FirstName = "Asd",
                LastName = "Asdeer",
                Email = "a@b.com",
                Password = "asdf1234",
                Avatar = "https://picsum.photos/200",
                Role = UserRole.Admin,
                Addresses = new List<Address>()
            };
            Add(UserRole.Admin, user, userExpected, GetMapper().Map<User, UserReadDTO>(userExpected), null);
            Add(UserRole.Admin, null, null, null, typeof(CustomException));
        }
    }
}

