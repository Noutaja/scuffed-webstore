using AutoMapper;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Address, AddressReadDTO>();
            CreateMap<AddressCreateDTO, Address>();
            CreateMap<AddressUpdateDTO, Address>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Image, ImageReadDTO>();
            CreateMap<ImageCreateDTO, Image>();
            CreateMap<ImageUpdateDTO, Image>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<OrderProduct, OrderProductReadDTO>();
            CreateMap<OrderProductCreateDTO, OrderProduct>();
        }
    }
}