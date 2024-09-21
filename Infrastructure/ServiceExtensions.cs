using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repos;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IEmployeeRepo>(provider => new EmployeeRepo(connectionString));
            services.AddTransient<IDepartmentsRepo>(provider => new DepartmentsRepo(connectionString));
        }
    }
}

