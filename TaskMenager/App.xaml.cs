using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using TaskMenager.Data;
using Microsoft.Extensions.Hosting;
using TaskMenager.Services;


namespace TaskMenager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<TaskContext>(options =>
                        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

                    services.AddScoped<ITaskService, TaskService>();
                    services.AddScoped<CreateTask>(); 
                    services.AddScoped<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>(); 
            mainWindow.Show();
            await _host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
