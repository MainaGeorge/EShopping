using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Response;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class GetQueryByUserNameHandler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetQueryByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserName(request.UserName);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
