using Microsoft.EntityFrameworkCore;
using PacificBattle.Components;
using PacificBattle.Data;
using PacificBattle.Interfaces;
using PacificBattle.Managers;
using Serilog;

namespace PacificBattle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Create Builder
                var builder = WebApplication.CreateBuilder(args);

                // Configure Logging
                builder.Services.AddSerilog((config) => config
                        .ReadFrom.Configuration(builder.Configuration));

                // Add Razor
                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

                // Add Controllers
                builder.Services.AddControllers();

                // Add Other Services
                builder.Services.AddSingleton<IFleetManager, FleetManager>();

                // Add HttpClient
                builder.Services.AddHttpClient<IFleetManager, FleetManager> (client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7156");
                });

                // Add DbContext
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                    options.UseSqlite($"Data Source={dbPath}");
                });

                // Build
                var app = builder.Build();
                Log.Information("------------------------------");
                Log.Information("");
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
                Log.Information(ex,"Doh! Unexpected Termination!");
            }
            finally
            {
                Log.Information("... Exiting.");
                Log.CloseAndFlush();
            }
        }
    }
}
