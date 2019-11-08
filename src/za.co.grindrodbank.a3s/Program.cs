/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;
using Sentry;

namespace za.co.grindrodbank.a3s
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // The application configuration needs to be manually created and bound here. DI does not work yet.
            var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false);
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != null)
            {
                configBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            }
            
            var config = configBuilder.Build();
            var sentryEnabled = bool.Parse(config["Sentry:Enabled"]);

            if (sentryEnabled)
            {
                using (SentrySdk.Init(config["Sentry:Dsn"]))
                {
                    StartApp(args);
                }
            }
            else
            {
                StartApp(args);
            }
            
        }

        public static void StartApp(string[] args)
        {
            NLogBuilder.ConfigureNLog("nlog.config");

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
