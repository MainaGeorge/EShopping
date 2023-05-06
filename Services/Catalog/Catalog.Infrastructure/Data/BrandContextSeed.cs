using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandsCollection)
        {
            bool checkIfSeeded = brandsCollection.Find(t => true).Any();
            string path = Path.Combine("Data", "SeedData", "brands.json");

            if (checkIfSeeded) return;

            var brandsData = File.ReadAllText(path);
            var brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);

            if (brands == null) return;

            foreach (var brand in brands)
                brandsCollection.InsertOneAsync(brand);

        }
    }
}
