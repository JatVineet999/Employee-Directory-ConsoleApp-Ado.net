using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Presentation.Interfaces;
using Presentation.MenuManagers;
using Presentation.Utility;

namespace Presentation
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Registering menu managers
            services.AddTransient<IMainMenuManager, MainMenuManager>();

            services.AddTransient<Lazy<IEmployeesMenuManager>>(provider =>
                new Lazy<IEmployeesMenuManager>(() =>
                    new EmployeesMenuManager(
                        provider.GetRequiredService<IMainMenuManager>(),
                        provider.GetRequiredService<Lazy<IRolesMenuManager>>().Value,
                        provider.GetRequiredService<IInputReader>(),
                        provider.GetRequiredService<IEmployeeServices>()
                    )
                )
            );

            services.AddTransient<Lazy<IRolesMenuManager>>(provider =>
                new Lazy<IRolesMenuManager>(() =>
                    new RolesMenuManager(
                        provider.GetRequiredService<IMainMenuManager>(),
                        provider.GetRequiredService<IDepartmentAndRolesServices>()
                    )
                )
            );
            services.AddTransient<IInputReader, InputReader>();
        }
    }
}

