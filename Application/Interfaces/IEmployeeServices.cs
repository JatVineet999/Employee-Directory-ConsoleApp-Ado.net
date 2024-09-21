using Infrastructure.Models;
namespace Application.Interfaces
{
    public interface IEmployeeServices
    {
        bool AddEmployeeRecord(Employee employee);
        Employee? GetEmployeeByEmployeeNumber(string employeeNumber);
        bool SaveUpdatedEmployeeData<T>(Employee employeeToUpdate, T userInput, string propertyType);
        List<Employee> GetEmployeeRecords();
        List<(Department, List<Role>)>? GetDepartmentAndRolesData();
        bool DeleteEmployee(string employeeNumber);
    }
}
