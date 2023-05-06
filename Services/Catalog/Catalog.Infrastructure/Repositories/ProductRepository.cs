using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IBrandRepository, IProductRepository, IProductTypeRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);

            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(p => p.Id, id);

            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
                .Brands
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllProductTypes()
        {
            return await _context
                .Types
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(p => p.Id, id);

            return await _context
                .Products
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(p => p.Brands.Name, brand);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(p => p.Name, name);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(p => p.Id, product.Id);

            var updateResult = await _context.Products.ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
