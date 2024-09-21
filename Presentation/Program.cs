using Microsoft.Extensions.DependencyInjection;
using Presentation.Interfaces;
using System.Configuration;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;

            var services = new ServiceCollection();
            Infrastructure.ServiceExtensions.ConfigureServices(services, connectionString);
            Application.ServiceExtensions.ConfigureServices(services);
            ServiceExtensions.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var mainMenuManager = serviceProvider.GetService<IMainMenuManager>();
            mainMenuManager?.DisplayMainMenu();
        }

    }
}
