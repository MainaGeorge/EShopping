using MediatR;
using Ordering.Application.Response;

namespace Ordering.Application.Queries
{
    public class GetOrdersByUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrdersByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
