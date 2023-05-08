using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsByBrandQuery : IRequest<IList<ProductResponse>>
    {
        public string BrandName { get; set; }

        public GetProductsByBrandQuery(string brandName)
        {
            BrandName = brandName;
        }
    }
}
