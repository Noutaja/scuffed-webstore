using AutoMapper;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(dest => dest.Addresses,
                    opt => opt.MapFrom(src => src.Addresses.Select(a =>
                    new AddressReadDTO { ID = a.ID, Street = a.Street, City = a.City, Zipcode = a.Zipcode, Country = a.Country })));
            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Password,
                    opt => opt.Ignore());
            /* CreateMap<UserUpdateDTO, User>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<Address, AddressReadDTO>();
            CreateMap<AddressCreateDTO, Address>();
            /* CreateMap<AddressUpdateDTO, Address>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<Product, ProductReadDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
                .Select(img => new ImageReadDTO { Url = img.Url, ID = img.ID, ProductID = img.ProductID })));

            CreateMap<Product, ProductWithoutPriceReadDTO>()
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
        .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
            .Select(img => new ImageReadDTO { Url = img.Url, ID = img.ID, ProductID = img.ProductID })));

            CreateMap<ProductCreateDTO, Product>()
                //.ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Images,
                     opt => opt.MapFrom(src => src.Images.Select(i =>
                new Image { Url = i.Url })));

            /* CreateMap<ProductUpdateDTO, Product>()
            .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<Image, ImageReadDTO>();
            CreateMap<ImageCreateDTO, Image>();
            /* CreateMap<ImageUpdateDTO, Image>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            /* CreateMap<CategoryUpdateDTO, Category>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.OrderProducts,
                    opt => opt.MapFrom(src => src.OrderProducts));
            /* CreateMap<OrderUpdateDTO, Order>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)); */

            CreateMap<OrderProduct, OrderProductReadDTO>();
            CreateMap<OrderProductCreateDTO, OrderProduct>();
        }
    }
}