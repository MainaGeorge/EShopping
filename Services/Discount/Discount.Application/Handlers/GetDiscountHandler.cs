using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _repository;

        public GetDiscountHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            return Mappings.Mapper.Map<CouponModel>(coupon);
        }
    }
}
