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
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                options.UseSqlite($"Data Source={dbPath}");
            });

            builder.Services.AddSingleton<AttackCoordinator>();


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
            Log.Information("Starting PacificBattle...");

            app.Run();
        }
    }
}
