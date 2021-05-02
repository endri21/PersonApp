using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PersonCore.Configurations;
using Microsoft.Extensions.DependencyInjection;
using PersonCore.Interfaces;

namespace PersonApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;


        /// <summary>
        /// Implementimi i  Dependency Injections me ane te  Autofac
        /// Konfigurimet behen ne DIBuilder.cs
        /// </summary>
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
                {
                    ConfigureServices(service);
                    DIBuilder.ConfigureSettingsServices(service);

                })
                .Build();
        }

        protected  void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var mainWind = _host.Services.GetRequiredService<MainWindow>();
            mainWind.Show();
            base.OnStartup(e);
        }

        protected override async void  OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }


      
    }
}
