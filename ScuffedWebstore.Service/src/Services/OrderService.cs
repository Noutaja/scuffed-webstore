using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class OrderService : BaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>, IOrderService
{
    private IUserRepo _userRepo;
    private IProductRepo _productRepo;

    public OrderService(IOrderRepo repo, IUserRepo userRepo, IProductRepo productRepo, IMapper mapper) : base(repo, mapper)
    {
        _userRepo = userRepo;
        _productRepo = productRepo;
    }

    public override OrderReadDTO CreateOne(OrderCreateDTO createObject)
    {
        User? u = _userRepo.GetOneById(createObject.UserID);
        if (u == null) throw CustomException.NotFoundException("User not found");

        foreach (OrderProductCreateDTO dto in createObject.OrderProducts)
        {
            Product? p = _productRepo.GetOneById(dto.ProductID);
            if (p == null) throw CustomException.NotFoundException("Product not found");
        }

        return _mapper.Map<Order, OrderReadDTO>(_repo.CreateOne(_mapper.Map<OrderCreateDTO, Order>(createObject)));
    }
}