using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.ApiGateway
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddCacheManager(o => o.WithDictionaryHandle());
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Ocelot");
                });
            });

            await app.UseOcelot();
        }
    }
}
