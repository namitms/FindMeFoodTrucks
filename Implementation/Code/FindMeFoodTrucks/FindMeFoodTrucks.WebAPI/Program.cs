using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                                          Host.CreateDefaultBuilder(args)
                                            .ConfigureLogging((hostingContext, builder) =>
                                            {
                                                try
                                                {
                                                    var appInsightskey = hostingContext.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

                                                    if (appInsightskey != null)
                                                    {
                                                        // Providing an instrumentation key here is required if you're using
                                                        // standalone package Microsoft.Extensions.Logging.ApplicationInsights
                                                        // or if you want to capture logs from early in the application startup
                                                        // pipeline from Startup.cs or Program.cs itself.
                                                        builder.AddApplicationInsights(appInsightskey);

                                                        // Adding the filter below to ensure logs of all severity from Program.cs
                                                        // is sent to ApplicationInsights.
                                                        builder.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                                                                         (typeof(Program).FullName, LogLevel.Trace);

                                                        // Adding the filter below to ensure logs of all severity from Startup.cs
                                                        // is sent to ApplicationInsights.
                                                        builder.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                                                                         (typeof(Startup).FullName, LogLevel.Trace);
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    //Log error. This should not stop the application from proceeding
                                                    throw;
                                                }
                                            })
                                            .ConfigureWebHostDefaults(webBuilder =>
                                            {
                                                webBuilder.UseStartup<Startup>();
                                            });
    }
}
