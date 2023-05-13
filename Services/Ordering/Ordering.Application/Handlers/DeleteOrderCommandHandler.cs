using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id) ?? throw new OrderNotFoundException(nameof(Order), request.Id);
            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"order number {orderToDelete.Id} successfully deleted");
        }
    }
}
