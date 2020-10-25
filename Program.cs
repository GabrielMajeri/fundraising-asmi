using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Asmi.Fundraising
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());

            var host = hostBuilder.Build();
            host.Run();
        }
    }
}
