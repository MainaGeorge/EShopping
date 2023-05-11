using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _repository;

        public DeleteDiscountHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteDiscount(request.ProductName);
        }
    }
}
