using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    internal class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
    {
        private readonly IProductTypeRepository _repository;

        public GetAllTypesHandler(IProductTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetAllProductTypes();

            return Mappings.Mapper.Map<IList<TypesResponse>>(types);
        }
    }
}
