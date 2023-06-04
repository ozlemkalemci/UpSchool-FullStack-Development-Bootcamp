using Application.Models.Order;
using Application.Models.OrderEvent;
using Application.Models.Product;
using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.AutoMapper
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderEvent, OrderEventDto>();
        }
    }
}
