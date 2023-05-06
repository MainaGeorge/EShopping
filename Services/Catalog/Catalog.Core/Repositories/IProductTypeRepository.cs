using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllProductTypes();
    }
}
