using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _repository;

        public CreateDiscountHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = Mappings.Mapper.Map<Coupon>(request);
            await _repository.CreateDiscount(coupon);

            coupon = await _repository.GetDiscount(request.ProductName);

            return Mappings.Mapper.Map<CouponModel>(coupon);
        }
    }
}
