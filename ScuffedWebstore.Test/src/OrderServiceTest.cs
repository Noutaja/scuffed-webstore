using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
using Xunit.Abstractions;

namespace ScuffedWebstore.Test.src;
public class OrderServiceTest
{
    private ITestOutputHelper _output;
    public OrderServiceTest(ITestOutputHelper output)
    {
        _output = output;
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
    [ClassData(typeof(GetAllOrdersData))]
    public async void GetAll_ShouldReturnValidResponse(IEnumerable<Order> response, IEnumerable<OrderReadDTO> expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());

        IEnumerable<OrderReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllOrdersData : TheoryData<IEnumerable<Order>, IEnumerable<OrderReadDTO>>
    {
        public GetAllOrdersData()
        {
            IEnumerable<Order> orders = new List<Order>();
            Add(orders, GetMapper().Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturnValidResponse(Order response, OrderReadDTO expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());

        OrderReadDTO result = await service.GetOneByIDAsync(It.IsAny<Guid>());

        Assert.Equivalent(expected, result);
    }

    public class GetOneByIDData : TheoryData<Order, OrderReadDTO>
    {
        public GetOneByIDData()
        {
            Order order = new Order
            {
                AddressID = It.IsAny<Guid>(),
                Status = It.IsAny<OrderStatus>(),
                OrderProducts = new List<OrderProduct>()
            };
            Add(order, GetMapper().Map<Order, OrderReadDTO>(order));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneOrderData))]
    public async void CreateOne_ShouldReturnValidResponse(User foundUser, Product foundProduct, OrderCreateDTO input, Order response, OrderReadDTO expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Order>())).Returns(Task.FromResult(response));
        userRepo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        productRepo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundProduct));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());
        OrderReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    public class CreateOneOrderData : TheoryData<User, Product, OrderCreateDTO, Order, OrderReadDTO>
    {
        public CreateOneOrderData()
        {
            OrderCreateDTO orderInput = new OrderCreateDTO()
            {
                AddressID = It.IsAny<Guid>(),
                OrderProducts = new List<OrderProductCreateDTO>() {
                         new OrderProductCreateDTO { ProductID = It.IsAny<Guid>(), Amount = 1 } }
            };
            Order order = GetMapper().Map<OrderCreateDTO, Order>(orderInput);
            User user = new User();
            Product product = new Product();
            Add(user, product, orderInput, order, GetMapper().Map<Order, OrderReadDTO>(order));
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());
        bool result = await service.DeleteOneAsync(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneOrderData))]
    public async void UpdateOne_ShouldReturnValidResponse(OrderUpdateDTO? input, Order? foundUser, Order? response, OrderReadDTO? expected, Type? exception)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Order>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());
        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            OrderReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneOrderData : TheoryData<OrderUpdateDTO?, Order?, Order?, OrderReadDTO?, Type?>
    {
        public UpdateOneOrderData()
        {
            OrderUpdateDTO orderInput = new OrderUpdateDTO()
            {
                Status = It.IsAny<OrderStatus>()
            };

            Order order = new Order
            {
                AddressID = It.IsAny<Guid>(),
                Status = It.IsAny<OrderStatus>(),
                OrderProducts = new List<OrderProduct>()
            };
            //Order order = GetMapper().Map<OrderUpdateDTO, Order>(orderInput);
            Add(orderInput, order, order, GetMapper().Map<Order, OrderReadDTO>(order), null);
            Add(orderInput, null, null, null, typeof(CustomException));
        }
    }
}
