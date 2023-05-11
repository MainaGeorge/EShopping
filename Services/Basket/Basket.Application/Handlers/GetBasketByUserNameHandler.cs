using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Response;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserName, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserName request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasket(request.UserName);
            return Mappings.Mapper.Map<ShoppingCartResponse>(basket);
        }
    }
}
