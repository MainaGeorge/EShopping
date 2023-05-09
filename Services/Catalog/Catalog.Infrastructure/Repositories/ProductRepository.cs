using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System.Linq.Expressions;

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

        public async Task<Pagination<Product>> GetProducts(CatalogSpecificationsParameters specifications)
        {
            var filter = BuildFilter(specifications);

            return specifications.Sort switch
            {
                "priceAsc" => await Paginate(filter, c => c.Price, specifications),
                "priceDesc" => await Paginate(filter, c => c.Price, specifications, "desc"),
                _ => await Paginate(filter, c => c.Name, specifications)
            };
        }

        private static FilterDefinition<Product> BuildFilter(CatalogSpecificationsParameters specifications)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(specifications.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(specifications.Search));
                filter &= searchFilter;
            }

            if (!string.IsNullOrEmpty(specifications.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, specifications.TypeId);
                filter &= typeFilter;
            }

            if (!string.IsNullOrEmpty(specifications.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, specifications.BrandId);
                filter &= brandFilter;
            }

            return filter;
        }
        private async Task<Pagination<Product>> Paginate(FilterDefinition<Product> filter, Expression<Func<Product, object>> sort,
            CatalogSpecificationsParameters specifications, string direction="asc")
        {
            return new Pagination<Product>
            {
                Count = await _context
                        .Products
                        .CountDocumentsAsync(_ => true),
                Data = await _context
                        .Products
                        .Find(filter)
                        .Sort(direction == "asc" ? Builders<Product>.Sort.Ascending(sort) : Builders<Product>.Sort.Descending(sort))
                        .Skip(specifications.PageSize * (specifications.PageIndex - 1))
                        .Limit(specifications.PageSize)
                        .ToListAsync(),
                PageIndex = specifications.PageIndex,
                PageSize = specifications.PageSize
            };
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
