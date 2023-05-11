using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
    {
        public GetAllProductsQuery(CatalogSpecificationsParameters specificationsParameters)
        {
            SpecificationsParameters = specificationsParameters;
        }

        public CatalogSpecificationsParameters SpecificationsParameters { get; }
    }
}
