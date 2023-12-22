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

    public override async Task<OrderReadDTO> CreateOneAsync(Guid id, OrderCreateDTO createObject)
    {
        User? u = await _userRepo.GetOneByIdAsync(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        foreach (OrderProductCreateDTO dto in createObject.OrderProducts)
        {
            Product? p = await _productRepo.GetOneByIdAsync(dto.ProductID);
            if (p == null) throw CustomException.NotFoundException("Product not found");
        }

        return _mapper.Map<Order, OrderReadDTO>(await _repo.CreateOneAsync(_mapper.Map<OrderCreateDTO, Order>(createObject)));
    }
}