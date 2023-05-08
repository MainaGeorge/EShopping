using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Mappings.Mapper.Map<Product>(request) ?? throw new ApplicationException("Something went wrong while creating a new product");

            var createdProduct = await _productRepository.CreateProduct(product);

            return Mappings.Mapper.Map<ProductResponse>(createdProduct);
        }
    }
}
