using Basket.Application.Commands;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository _repository;

        public DeleteBasketByUserNameCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteBasket(request.UserName);
            return Unit.Value;
        }
    }
}
