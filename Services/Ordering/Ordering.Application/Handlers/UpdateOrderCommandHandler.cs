using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id) ?? throw new OrderNotFoundException(nameof(Order), request.Id);

            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"order number {orderToUpdate.Id} successfully updated");
        }
    }
}
