using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Ocelot.ApiGateway
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((env, config) =>
                    {
                        config.AddJsonFile($"ocelot.{env.HostingEnvironment.EnvironmentName}.json", true, true);
                    })
                .ConfigureWebHostDefaults(builder => builder.UseStartup<StartUp>());

        }
    }
}
