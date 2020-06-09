using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using TestDeploy.Api.Data;

namespace TestDeploy.Api.Config
{
    internal static class StartupConfigExtensions
    {
        internal static IServiceCollection ConfigureDb(this IServiceCollection services, string databaseUrl, IWebHostEnvironment env)
        {
            string connectionString = databaseUrl;
            if (env.IsProduction())
            {
                var dbUri = new Uri(databaseUrl);
                var userInfo = dbUri.UserInfo.Split(':');

                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = dbUri.Host,
                    Port = dbUri.Port,
                    Username = userInfo[0],
                    Passfile = userInfo[1],
                    Database = dbUri.LocalPath.TrimStart('/'),
                };

                connectionString = builder.ToString();
            }

            return services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}