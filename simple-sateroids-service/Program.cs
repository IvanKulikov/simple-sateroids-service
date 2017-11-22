using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace simple_sateroids_service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5000);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();                        
            host.Run();
        }
    }
}
