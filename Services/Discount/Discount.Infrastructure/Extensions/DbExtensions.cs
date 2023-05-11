using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("discount db migrations started");
                ApplyMigrations(config);
                logger.LogInformation("discount db migrations completed");
                return host;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }
        }

        private static void ApplyMigrations(IConfiguration config)
        {
            using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new NpgsqlCommand() { Connection = connection };
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(500) NOT NULL, Description TEXT, Amount INT NOT NULL)";
            command.ExecuteNonQuery();
            command.CommandText = @"INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'SHOE DISCOUNT', 500)";
            command.ExecuteNonQuery();
            command.CommandText = @"INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex Super Ace Light Badminton Shoes', 'RACQUET DISCOUNT', 500)";
            command.ExecuteNonQuery();
        }
    }
}
