using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository
               .GetProductsByName(request.Name);

            return Mappings.Mapper.Map<IList<ProductResponse>>(products);
        }
    }
}
