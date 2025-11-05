using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

using projectIost.Models;
using projectIost.Data;
using projectIost.Services;
using projectIost.Views;

namespace projectIost
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Register DbContext
            var conn = builder.Configuration.GetConnectionString("DefaultConnection")
                   ?? "Server=127.0.0.1;Database=iostdb;User=root;Password=;";

            builder.Services.AddDbContext<IostDbContext>(options =>
                options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

            // Register application services and forms
            builder.Services.AddScoped<IIostService, IostService>();

            // Removed legacy controller registration (IostController) — it's not used anymore.
            // Register InventoryView so it can be constructed by DI and receive IIostService automatically.
            builder.Services.AddScoped<InventoryView>();

            // MainForm will be resolved by DI
            builder.Services.AddScoped<LoginView>();

            builder.Services.AddScoped<RegisterView>();

            builder.Services.AddScoped<SupplyView>();

            builder.Services.AddScoped<OrderView>();

            builder.Services.AddScoped<AnalyticsView>();


            var host = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var scope = host.Services.CreateScope())
            {
                var mainForm = scope.ServiceProvider.GetRequiredService<LoginView>();
                Application.Run(mainForm);
            }
        }
    }
}