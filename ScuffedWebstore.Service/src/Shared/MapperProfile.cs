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
                    new AddressReadDTO { UserID = a.UserID, ID = a.ID, Street = a.Street, City = a.City, Zipcode = a.Zipcode, Country = a.Country })));
            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Password,
                    opt => opt.Ignore());
            CreateMap<UserUpdateDTO, User>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Address, AddressReadDTO>();
            CreateMap<AddressCreateFullDTO, Address>();
            CreateMap<AddressCreateBasicDTO, AddressCreateFullDTO>();
            CreateMap<AddressUpdateDTO, Address>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Product, ProductReadDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
                .Select(img => new ImageReadDTO { Url = img.Url, ID = img.ID, ProductID = img.ProductID })));

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Images,
                     opt => opt.MapFrom(src => src.Images.Select(i =>
                new Image { Url = i.Url })));

            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Images,
                    opt => opt.MapFrom(src => src.Images))
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Image, ImageReadDTO>();
            CreateMap<ImageCreateDTO, Image>();
            CreateMap<ImageUpdateDTO, Image>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<Order, OrderReadDTO>()
                .ForMember(dest => dest.OrderProducts,
                    opt => opt.MapFrom(src => src.OrderProducts
                        .Select(op => new OrderProductReadDTO { ProductID = op.ProductID, OrderID = op.OrderID, Price = op.Price })));
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.OrderProducts,
                    opt => opt.MapFrom(src => src.OrderProducts));
            CreateMap<OrderUpdateDTO, Order>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<OrderProduct, OrderProductReadDTO>();
            CreateMap<OrderProductCreateDTO, OrderProduct>();
        }
    }
}