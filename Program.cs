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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("data/logs/log.txt", 
                    rollingInterval: RollingInterval.Day, 
                    rollOnFileSizeLimit: true,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss [{Level}] -- {Message}{NewLine}{Exception}")
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                options.UseSqlite($"Data Source={dbPath}");
            });

            builder.Services.AddSingleton<Roller>();
            builder.Services.AddSingleton<AttackResolver>();


            var app = builder.Build();

            // Configure HTTP Request Pipeline
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
            Log.Information("Battle Started...");

            app.Run();
        }
    }
}
