using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IEmployeeRepo : ICommonRepo
    {
        List<Employee> LoadEmployeeRecords();
        bool SaveEmployeeRecord(Employee employee);
        bool RemoveEmployee(string employeeNumber);
        bool UpdateEmployee(Employee updatedEmployee);


    }
}
