using MediatR;
using Ordering.Application.Response;

namespace Ordering.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
