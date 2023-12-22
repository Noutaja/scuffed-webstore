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
    public void GetAll_ShouldReturnValidResponse(IEnumerable<User> response, IEnumerable<UserReadDTO> expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAll(options)).Returns(response);
        UserService service = new UserService(repo.Object, GetMapper());

        IEnumerable<UserReadDTO> result = service.GetAll(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllUsersData : TheoryData<IEnumerable<User>, IEnumerable<UserReadDTO>>
    {
        public GetAllUsersData()
        {
            User user1 = new User() { FirstName = "Asd", LastName = "Asdeer", Email = "a@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            User user2 = new User() { FirstName = "Qwe", LastName = "Qwerty", Email = "q@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            User user3 = new User() { FirstName = "Zxc", LastName = "Zxcvbn", Email = "z@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            IEnumerable<User> users = new List<User>() { user1, user2, user3 };
            Add(users, GetMapper().Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public void GetOneByID_ShouldReturnValidResponse(User response, UserReadDTO expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetOneById(It.IsAny<Guid>())).Returns(response);
        UserService service = new UserService(repo.Object, GetMapper());

        UserReadDTO result = service.GetOneByID(It.IsAny<Guid>());

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
                Addresses = new List<Address>()  //Is making an empty list here right? It's null otherwise but the DTO is []
            };
            Add(user, GetMapper().Map<User, UserReadDTO>(user));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneUserData))]
    public void CreateOne_ShouldReturnValidResponse(UserCreateDTO input, User response, UserReadDTO expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.CreateOne(It.IsAny<User>())).Returns(response);
        UserService service = new UserService(repo.Object, GetMapper());

        UserReadDTO result = service.CreateOne(input);

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
    public void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.DeleteOne(It.IsAny<Guid>())).Returns(response);
        UserService service = new UserService(repo.Object, GetMapper());

        bool result = service.DeleteOne(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneUserData))]
    public void UpdateOne_ShouldReturnValidResponse(UserUpdateDTO? input, User? foundUser, User? response, UserReadDTO? expected, Type? exception)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.UpdateOne(It.IsAny<User>())).Returns(response);
        repo.Setup(repo => repo.GetOneById(It.IsAny<Guid>())).Returns(foundUser);
        UserService service = new UserService(repo.Object, GetMapper());

        if (exception != null) Assert.Throws(exception, () => service.UpdateOne(It.IsAny<Guid>(), input));
        else
        {
            UserReadDTO result = service.UpdateOne(It.IsAny<Guid>(), input);

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
            User user = GetMapper().Map<UserUpdateDTO, User>(userInput);
            Add(userInput, user, user, GetMapper().Map<User, UserReadDTO>(user), null);
            Add(userInput, null, null, null, typeof(CustomException));
        }
    }

    [Theory]
    [ClassData(typeof(UpdateRoleData))]
    public void UpdateRole_ShouldReturnValidResponse(UserRole input, User? foundUser, User? response, UserReadDTO? expected, Type? exception)
    {
        Mock<IUserRepo> repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.UpdateOne(It.IsAny<User>())).Returns(response);
        repo.Setup(repo => repo.GetOneById(It.IsAny<Guid>())).Returns(foundUser);
        UserService service = new UserService(repo.Object, GetMapper());

        if (exception != null) Assert.Throws(exception, () => service.UpdateRole(It.IsAny<Guid>(), input));
        else
        {
            UserReadDTO result = service.UpdateRole(It.IsAny<Guid>(), input);

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

