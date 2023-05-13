using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Response;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            var createdOrder = await _orderRepository.AddAsync(order);
            _logger.LogInformation($"order number {createdOrder.Id} created successfully");

            return _mapper.Map<OrderResponse>(createdOrder);
        }
    }
}
