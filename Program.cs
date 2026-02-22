using Microsoft.EntityFrameworkCore;
using PacificBattle.Components;
using PacificBattle.Data;
using PacificBattle.Interfaces;
using PacificBattle.Managers;
using Serilog;
using Serilog.Events;

namespace PacificBattle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Logger
            var loggingConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Initialize Bootstrap Logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: "{Timestamp: ddd-MMM-dd HH:mm:ss} [{Level:u3}] -- {Message}{NewLine}{Exception}")
                .CreateBootstrapLogger();
            Log.Information("Starting App...");

            try
            {
                // Create Builder
                var builder = WebApplication.CreateBuilder(args);

                // Replace Bootstrap Logger with Host Logger
                builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
                {
                    // Configure Logger
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext();
                },
                preserveStaticLogger: false);
                Log.Information("BootstrapLogger Replaced, Configuring App...");


                // Add Razor
                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

                // Add Controllers
                builder.Services.AddControllers();

                // Register Services
                builder.Services.AddSingleton<IFleetManager, FleetManager>();

                // Add HttpClient
                builder.Services.AddHttpClient<IFleetManager, FleetManager> (client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7156");
                });

                // Register DbContext
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                    options.UseSqlite($"Data Source={dbPath}");
                });

                // Build App
                var app = builder.Build();
                Log.Information("Initializing...");
                Log.Information("Application Built.");

                // Configure HTTP request pipeline
                app.UseSerilogRequestLogging();

                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();

                app.UseStaticFiles();
                app.UseAntiforgery();

                app.MapRazorComponents<App>()
                    .AddInteractiveServerRenderMode();

                app.MapControllers();

                Log.Information("Starting...");
                Log.Information("");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Doh! Unexpected Termination! ... Exiting ... Buh-bye.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
