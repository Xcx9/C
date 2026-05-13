using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ThreadProfiler.Services;
using ThreadProfiler.ViewModels;
using ThreadProfiler.Views;

namespace ThreadProfiler
{
    public class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITaskSimulator, TaskSimulator>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>();
            
            ServiceProvider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var window = ServiceProvider.GetRequiredService<MainWindow>();
                window.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = window;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
