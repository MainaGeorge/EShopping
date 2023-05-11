using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<Coupon, CreateDiscountCommand>().ReverseMap();    
            CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();
        }
    }
}
