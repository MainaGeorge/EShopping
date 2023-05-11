using Basket.Application.Response;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketByUserName : IRequest<ShoppingCartResponse>
    {
        public GetBasketByUserName(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}
