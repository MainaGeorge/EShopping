using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Response;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateBasketByUserNameCommandHandler : IRequestHandler<CreateBasketByUserNameCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateBasketByUserNameCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(CreateBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.UpdateBasket(new Core.Entities.ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items,
            });

            return Mappings.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
