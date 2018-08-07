using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sprotify.Data.EF;

namespace Sprotify.WebApi
{
    public class Program
    {
        private static IHostingEnvironment _env;

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, _) => _env = ctx.HostingEnvironment)
                .CaptureStartupErrors(_env?.IsDevelopment() ?? false)
                .UseStartup<Startup>();

        public static IWebHost BuildWebHost(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            if (_env?.IsDevelopment() == true)
            {
                using (var scope = webHost.Services.CreateScope())
                {
                    var dbc = scope.ServiceProvider.GetRequiredService<SprotifyDbContext>();
                    dbc.Database.EnsureCreated();
                    // Use `dbc.Database.Migrate();` instead when using migrations!
                }
            }

            return webHost;
        }
    }
}
