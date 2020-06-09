using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestDeploy.Api.Data;
using TestDeploy.Api.Entities;

namespace TestDeploy.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                await InitializeDbAsync(scope.ServiceProvider);
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            
        private static async Task InitializeDbAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            await db.Database.MigrateAsync();

            db.Things.AddRange(new[]
            {
                new Thing
                {
                    Name = "Thing 1",
                },
                new Thing
                {
                    Name = "Thing 2",
                },
            });

            await db.SaveChangesAsync();
        }
    }
}
