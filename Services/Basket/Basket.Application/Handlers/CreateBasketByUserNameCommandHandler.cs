using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Response;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateBasketByUserNameCommandHandler : IRequestHandler<CreateBasketByUserNameCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateBasketByUserNameCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var discount = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= discount.Amount;
            }

            var shoppingCart = await _basketRepository.UpdateBasket(new Core.Entities.ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items,
            });

            return Mappings.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
