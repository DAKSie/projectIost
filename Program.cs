using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

using projectIost.Models;
using projectIost.Data;
using projectIost.Services;

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

            //Register DbContext
            var conn = builder.Configuration.GetConnectionString("DefaultConnection")
                   ?? "Server=127.0.0.1;Database=iostdb;User=root;Password=;";

            builder.Services.AddDbContext<IostDbContext>(options =>
                options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

            //register app services, controllers, forms
            builder.Services.AddScoped<IIostService, IostService>();
            builder.Services.AddScoped<IostController>();
            builder.Services.AddScoped<MainForm>();

            var host = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            using (var scope = host.Services.CreateScope())
            {
                var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
            //ApplicationConfiguration.Initialize();
        }
    }
}