using Files;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
namespace WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFolder>(new Folder());
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

    public enum SearchOptions
    {
       CurrentDirectory = System.IO.SearchOption.TopDirectoryOnly,
       AllDirectories = System.IO.SearchOption.AllDirectories

    }
}
