using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _repository;

        public UpdateDiscountHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateDiscount(Mappings.Mapper.Map<Coupon>(request));

            var coupon = _repository.GetDiscount(request.ProductName);

            return Mappings.Mapper.Map<CouponModel>(coupon);
        }
    }
}
