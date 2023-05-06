using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductTypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typesCollection)
        {
            bool checkIfSeeded = typesCollection.Find(t => true).Any();
            string path = Path.Combine("Data", "SeedData", "types.json");

            if (checkIfSeeded) return;

            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);

            if (types == null) return;

            foreach (var type in types)
                typesCollection.InsertOneAsync(type);

        }
    }
}
