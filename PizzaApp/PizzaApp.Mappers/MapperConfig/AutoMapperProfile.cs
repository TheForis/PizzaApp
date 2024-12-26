using AutoMapper;
using PizzaApp.DomainModels.Entites;
using PizzaApp.DTOs.OrderDtos;
using PizzaApp.DTOs.PizzaDtos;
using PizzaApp.DTOs.UserDtos;

namespace PizzaApp.Mappers.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoginUserRequestDto>().ReverseMap();
            CreateMap<User, RegisterUserRequestDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();

            CreateMap<Pizza, PizzaDto>().ReverseMap();
            CreateMap<Pizza, AddPizzaDto>().ReverseMap();
            CreateMap<Pizza, UpdatePizzaDto>().ReverseMap();
            CreateMap<AddPizzaDto, PizzaDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, AddOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
        }
    }
}
