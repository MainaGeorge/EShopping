using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

namespace Ordering.API.Extensions
{
    public static class OrderExtensions
    {
        public static IHost MigrateDataBase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation($"Started Db migrations {nameof(TContext)}");
                SeedData(seeder, context, services);
                logger.LogInformation($"Migration Completed {nameof(TContext)}");
            }
            catch (Exception e)
            {
                logger.LogError(e, $"An error occurred while migrating database {nameof(TContext)}");
            }
            return host;
        }

        private static void SeedData<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }

        public static IHost SeedDataBase(this IHost host, WebApplication app)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<OrderContext>();

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(OrderContextSeed.GetOrders());
                context.SaveChanges();
            }
            return host;
        }
    }
}
