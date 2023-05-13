using AutoMapper;
using Ordering.Application.Commands;
using Ordering.Application.Response;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers
{
    public class OrderMappingProfiles : Profile
    {
        public OrderMappingProfiles()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
        }
    }
}
