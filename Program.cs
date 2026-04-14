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
            // Initialize Bootstrap Logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: "{Timestamp: ddd-MMM-dd HH:mm:ss} [{Level:u3}] -- {Message}{NewLine}{Exception}")
                .CreateBootstrapLogger();
            Log.Information("Running Pacific Battle...");

            try
            {
                // Create Builder
                var builder = WebApplication.CreateBuilder(args);

                // Replace Bootstrap Logger with Host Logger
                builder.Logging.ClearProviders();
                builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
                {
                    // Configure Logger
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext();
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
                    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                    options.UseSqlite($"Data Source={dbPath}");
                });

                // Build App
                var app = builder.Build();

                // Configure HTTP request pipeline
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

                Log.Information("*****************************************");
                Log.Information("Commencing battle");
                Log.Information("");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Whoops... Unexpected Termination... FUBAR ... Exiting ... Buh-bye.");
            }
            finally
            {
                Log.Information("Closing Remarks");
                Log.CloseAndFlush();
            }
        }
    }
}
