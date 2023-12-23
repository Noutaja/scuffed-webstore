using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;
using ScuffedWebstore.Service.src.Shared;
using Xunit;

namespace ScuffedWebstore.Test.src
{
    public class AuthServiceTest
    {
        private static IMapper GetMapper()
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(m =>
                {
                    m.AddProfile(new MapperProfile());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
        /* [Theory]
        [ClassData(typeof(LoginData))]
        public async void Login_ShouldReturnValidResponse(User? foundUser, Credentials creds, string response, string expected, Type? exception)
        {
            Mock<IUserRepo> repo = new Mock<IUserRepo>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();
            Mock<PasswordHandler> passwordHandler = new Mock<PasswordHandler>();
            repo.Setup(repo => repo.GetOneByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(foundUser));
            tokenService.Setup(service => service.GenerateToken(It.IsAny<User>())).Returns(response);
            
            AuthService service = new AuthService(repo.Object, tokenService.Object, GetMapper());


            if (exception != null) await Assert.ThrowsAsync(exception, () => service.LoginAsync(creds.Email, creds.Password));
            else
            {
                string result = await service.LoginAsync(creds.Email, creds.Password);
                Assert.Equal(expected, result);
            }
        } */

        public class LoginData : TheoryData<User?, Credentials, string, string, Type?>
        {
            public LoginData()
            {
                Credentials credentials = new Credentials() { Email = "a@b.com", Password = "asdf1234" };
                User user = new User()
                {
                    FirstName = "Asd",
                    LastName = "Asdeer",
                    Email = "a@b.com",
                    Password = "asdf1234",
                    Salt = new byte[] { 1, 2 },
                    Role = It.IsAny<UserRole>(),
                    Addresses = new List<Address>(),
                    Orders = new List<Order>(),
                    Reviews = new List<Review>()
                };
                Add(user, credentials, "token", "token", null);
            }
        }

        [Theory]
        [ClassData(typeof(GetProfileData))]
        public async void GetProfileAsync_ShouldReturnValidResponse(User response, UserReadDTO expected)
        {
            Mock<IUserRepo> repo = new Mock<IUserRepo>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();
            repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
            AuthService service = new AuthService(repo.Object, tokenService.Object, GetMapper());

            UserReadDTO result = await service.GetProfileAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, result);
        }

        public class GetProfileData : TheoryData<User, UserReadDTO>
        {
            public GetProfileData()
            {
                User user = new User();
                Add(user, GetMapper().Map<User, UserReadDTO>(user));
            }
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void DeleteProfileAsync_ShouldReturnValidResponse(bool response, bool expected)
        {
            Mock<IUserRepo> repo = new Mock<IUserRepo>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();
            repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
            AuthService service = new AuthService(repo.Object, tokenService.Object, GetMapper());

            bool result = await service.DeleteProfileAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, result);
        }

    }
}