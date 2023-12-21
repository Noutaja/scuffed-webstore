using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;
using ScuffedWebstore.Service.src.Shared;
using Xunit;

namespace ScuffedWebstore.Test.src
{
    public class AddressServiceTest
    {
        public AddressServiceTest()
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
        [ClassData(typeof(GetAllAddressesData))]
        public void GetAll_ShouldReturnValidResponse(IEnumerable<Address> response, IEnumerable<AddressReadDTO> expected)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            GetAllParams options = new GetAllParams();
            repo.Setup(repo => repo.GetAll(options)).Returns(response);
            AddressService service = new AddressService(repo.Object, GetMapper());

            IEnumerable<AddressReadDTO> result = service.GetAll(options);

            Assert.Equivalent(expected, result);
        }

        [Theory]
        [ClassData(typeof(GetAllAddressesData))]
        public void GetAllWithSearch_ShouldReturnValidResponse(IEnumerable<Address> response, IEnumerable<AddressReadDTO> expected)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            GetAllUsersParams options = new GetAllUsersParams();
            repo.Setup(repo => repo.GetAll(options)).Returns(response);
            AddressService service = new AddressService(repo.Object, GetMapper());

            IEnumerable<AddressReadDTO> result = service.GetAll(options);

            Assert.Equivalent(expected, result);
        }

        public class GetAllAddressesData : TheoryData<IEnumerable<Address>, IEnumerable<AddressReadDTO>>
        {
            public GetAllAddressesData()
            {
                Address address1 = new Address() { City = "Some city", Country = "Some country", Zipcode = "12345", Street = "Street 1" };
                Address address2 = new Address() { City = "Some city", Country = "Some country", Zipcode = "12345", Street = "Street 2" };
                Address address3 = new Address() { City = "Some city", Country = "Some country", Zipcode = "12345", Street = "Street 3" };
                IEnumerable<Address> addresses = new List<Address>() { address1, address2, address3 };
                Add(addresses, GetMapper().Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(addresses));
            }
        }

        [Theory]
        [ClassData(typeof(GetOneByIDData))]
        public void GetOneByID_ShouldReturnValidResponse(Address response, AddressReadDTO expected)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            repo.Setup(repo => repo.GetOneById(It.IsAny<Guid>())).Returns(response);
            AddressService service = new AddressService(repo.Object, GetMapper());

            AddressReadDTO result = service.GetOneByID(It.IsAny<Guid>());

            Assert.Equivalent(expected, result);
        }

        public class GetOneByIDData : TheoryData<Address, AddressReadDTO>
        {
            public GetOneByIDData()
            {
                Address address = new Address() { City = "Some city", Country = "Some country", Zipcode = "12345", Street = "Street 1" };
                Add(address, GetMapper().Map<Address, AddressReadDTO>(address));
                Add(null, null);
            }
        }

        [Theory]
        [ClassData(typeof(CreateOneAddressData))]
        public void CreateOne_ShouldReturnValidResponse(AddressCreateDTO input, Address response, AddressReadDTO expected)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            repo.Setup(repo => repo.CreateOne(It.IsAny<Address>())).Returns(response);
            AddressService service = new AddressService(repo.Object, GetMapper());

            AddressReadDTO result = service.CreateOne(input);

            Assert.Equivalent(expected, result);
        }

        public class CreateOneAddressData : TheoryData<AddressCreateDTO, Address, AddressReadDTO>
        {
            public CreateOneAddressData()
            {
                AddressCreateDTO addressInput = new AddressCreateDTO() { UserID = It.IsAny<Guid>(), City = "Some city", Country = "Some country", Zipcode = "12345", Street = "Street 1" };
                Address address = GetMapper().Map<AddressCreateDTO, Address>(addressInput);
                Add(addressInput, address, GetMapper().Map<Address, AddressReadDTO>(address));
            }
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            repo.Setup(repo => repo.DeleteOne(It.IsAny<Guid>())).Returns(response);
            AddressService service = new AddressService(repo.Object, GetMapper());

            bool result = service.DeleteOne(It.IsAny<Guid>());

            Assert.Equal(expected, response);
        }

        [Theory]
        [ClassData(typeof(UpdateOneAddressData))]
        public void UpdateOne_ShouldReturnValidResponse(AddressUpdateDTO? input, Address? foundUser, Address? response, AddressReadDTO? expected, Type? exception)
        {
            Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
            repo.Setup(repo => repo.UpdateOne(It.IsAny<Address>())).Returns(response);
            repo.Setup(repo => repo.GetOneById(It.IsAny<Guid>())).Returns(foundUser);
            AddressService service = new AddressService(repo.Object, GetMapper());

            if (exception != null) Assert.Throws(exception, () => service.UpdateOne(It.IsAny<Guid>(), input));
            else
            {
                AddressReadDTO result = service.UpdateOne(It.IsAny<Guid>(), input);

                Assert.Equivalent(expected, result);
            }
        }

        public class UpdateOneAddressData : TheoryData<AddressUpdateDTO?, Address?, Address?, AddressReadDTO?, Type?>
        {
            public UpdateOneAddressData()
            {
                AddressUpdateDTO addressInput = new AddressUpdateDTO()
                {
                    City = "Some city",
                    Country = "Some country",
                    Zipcode = "12345",
                    Street = "Street 1"
                };
                Address address = GetMapper().Map<AddressUpdateDTO, Address>(addressInput);
                Add(addressInput, address, address, GetMapper().Map<Address, AddressReadDTO>(address), null);
                Add(addressInput, null, null, null, typeof(CustomException));
            }
        }
    }
}