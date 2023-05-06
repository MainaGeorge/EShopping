using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productsCollection)
        {
            bool checkIfSeeded = productsCollection.Find(t => true).Any();
            string path = Path.Combine("Data", "SeedData", "brands.json");

            if (checkIfSeeded) return;

            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);

            if (products == null) return;

            foreach (var product in products)
                productsCollection.InsertOneAsync(product);

        }
    }
}
