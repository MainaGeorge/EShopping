using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsByNameQuery : IRequest<IList<ProductResponse>>
    {
        public GetProductsByNameQuery(string name)
        {
            Name = name;

        }
        public string Name { get; set; }
    }
}
