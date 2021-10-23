using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

using Library.SqlServer;

namespace Library.Api
{
    /// <summary>
    /// The program class, holds the application entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">Launch arguments passed to the program.</param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(
                    "serverAPI_e_logs",
                    Serilog.Events.LogEventLevel.Error,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("Building host...");
                var host = CreateHostBuilder(args).Build();
                Log.Information("Host builded.");

                if (args.Any(x => x.Contains("migrate")))
                    MigrateDataBase(host);

                Log.Information("Host runing...");
                host.Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal("--Host stopped: {0}  \n\n --InnerException: {1}",
                    ex.Message,
                    ex.InnerException);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Create an instance for the application host builder.
        /// </summary>
        /// <param name="args">Launch arguments passed to the program.</param>
        /// <returns>Returns <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });

        /// <summary>
        /// Migrate the data base directly from code.
        /// </summary>
        /// <param name="host"></param>
        public static void MigrateDataBase(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var dbConfigContext = services.GetRequiredService<ApplicationDBContext>();

                Log.Information("Migratting SQLite database...");
                if (!dbConfigContext.Database.EnsureCreated())
                {
                    dbConfigContext.Database.Migrate();
                }
                Log.Information("SQLite database Migrated.");
            }
        }
    }
}
