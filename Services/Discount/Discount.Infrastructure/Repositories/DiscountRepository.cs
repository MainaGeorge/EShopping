using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRows = await connection.ExecuteAsync("INSERT INTO Coupon(ProductName, Description, Amount) VALUES(@productName, @description, @Amount)", 
                new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description=coupon.Description });

            return affectedRows > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("DELETE FROM coupon WHERE productName = @ProductName",
                new {  ProductName = productName });

            return affectedRows > 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE productName = @productName", new { productName });

            return coupon ?? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No discount availabe" };
        }

        public async  Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("UPDATE coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
                new { Id = coupon.Id, Amount = coupon.Amount, Description = coupon.Description, ProductName = coupon.ProductName });

            return affectedRows > 0;
        }
    }
}
