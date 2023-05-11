using Basket.Application.Response;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateBasketByUserNameCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public CreateBasketByUserNameCommand(string userName, List<ShoppingCartItem> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
