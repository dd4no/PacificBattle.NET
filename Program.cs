using Microsoft.EntityFrameworkCore;
using PacificBattle.Components;
using PacificBattle.Data;

namespace PacificBattle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Razor Services 
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "pacificbattle.db");
                options.UseSqlite($"Data Source={dbPath}");
            });

            var app = builder.Build();

            // Configure HTTP Request Pipeline.
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

            app.Run();
        }
    }
}
