using Basket.Application.Handlers;
using Basket.Core.Repository;
using Basket.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using System.Reflection;

namespace Basket.API
{
    public class StartUp
    {
        private readonly IConfiguration _configuration;

        public StartUp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Basket.API", Version = "v1" }));
            services.AddAutoMapper(typeof(StartUp));
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateBasketByUserNameCommandHandler).GetTypeInfo().Assembly));
            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = _configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            services.AddHealthChecks()
                .AddRedis(_configuration["CacheSettings:ConnectionString"], "Redis Health", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
