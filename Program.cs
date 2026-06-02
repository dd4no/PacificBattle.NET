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
            // Enable Bootstrap Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            Log.Information("Running Pacific Battle...");

            try
            {
                // Create Builder
                var builder = WebApplication.CreateBuilder(args);

                // Configure Logging Environment
                var logDir = Path.Combine(
                    builder.Environment.ContentRootPath,
                    "Logs");
                var logFile = Path.Combine(
                    logDir,
                    "PacificBattleLog-.txt");
                var errorFile = Path.Combine(
                    logDir,
                    "PacificBattleErrors-.txt");
                var logTemplate = 
                    "{Timestamp: ddd-MMM-dd HH:mm:ss} [{Level:u3}] -- {Message}{NewLine}{Exception}";
                Directory.CreateDirectory(logDir);

                // Replace Bootstrap Logger with Host Logger
                builder.Services.AddSerilog((services, logConfig) =>
                {
                    logConfig
                        .ReadFrom.Configuration(builder.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .WriteTo.File(
                            logFile,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            outputTemplate: logTemplate)
                        .WriteTo.File(
                            errorFile,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            outputTemplate: logTemplate
                        );
                },
                preserveStaticLogger: false);

                // Add Razor
                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

                // Add Controllers
                builder.Services.AddControllers();

                // Register Services
                builder.Services.AddScoped<IFleetManager, FleetManager>();

                // Register DbContext
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    var dbPath = Path.Combine(
                        builder.Environment.ContentRootPath, 
                        "Data", 
                        "pacificbattle.db"
                    );
                    options.UseSqlite($"Data Source={dbPath}");
                });

                // Build App
                var app = builder.Build();

                // Configure Middleware
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                }
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseAntiforgery();

                // Add Mappings
                app.MapRazorComponents<App>()
                    .AddInteractiveServerRenderMode();
                app.MapControllers();

                // Launch App
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Whoops... Unexpected Termination... FUBAR ... Exiting ... Buh-bye.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
