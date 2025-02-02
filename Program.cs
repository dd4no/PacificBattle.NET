using Microsoft.EntityFrameworkCore;
using PacificBattle.Classes;
using PacificBattle.Components;
using PacificBattle.Data;
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

                // Register Db
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                    options.UseSqlite($"Data Source={dbPath}");
                });

                // Add Other Services
                //builder.Services.AddSingleton<AttackCoordinator>();
                //builder.Services.AddSingleton<ShipGenerator>();

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
