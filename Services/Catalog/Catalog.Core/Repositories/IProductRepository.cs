using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecificationsParameters specifications);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<Product> GetById(string id);
        Task<IEnumerable<Product>> GetProductsByBrand(string brand);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
