using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ChaosInitiative.Web.HomePage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost builder = CreateHostBuilder(args).Build();
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}