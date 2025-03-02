using AutoMapper;
using DTOs;
using Entities;

namespace myProject
{
    public class AutoMapper:Profile 
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.UserName,opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName)).ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();

        }
    }
}
