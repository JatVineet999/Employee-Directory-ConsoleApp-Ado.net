using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Utility;
using Application.Interfaces;


namespace Application
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDepartmentAndRolesServices, DepartmentAndRolesServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IValidationService, ValidationService>();
        }
    }
}
