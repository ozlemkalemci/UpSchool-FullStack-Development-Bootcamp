using Application.Common.Models.Order;
using Application.Common.Models.OrderEvent;
using Application.Common.Models.Product;
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
