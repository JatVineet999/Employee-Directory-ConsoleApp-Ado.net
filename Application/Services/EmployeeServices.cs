using System.Reflection;
using Infrastructure.Models;
using Application.Interfaces;
using Infrastructure.Interfaces;
namespace Application.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeServices(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        private string GenerateEmployeeNumber()
        {
            Random rand = new Random();
            return $"TZ{rand.Next(1000, 10000)}";
        }

        public bool AddEmployeeRecord(Employee employee)
        {
            employee.EmployeeNumber = GenerateEmployeeNumber();
            bool isAdded = _employeeRepo.SaveEmployeeRecord(employee);
            return isAdded;
        }

        public Employee? GetEmployeeByEmployeeNumber(string employeeNumber)
        {
            var employeeRecords = _employeeRepo.LoadEmployeeRecords();
            if (employeeRecords != null)
            {
                return employeeRecords.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);
            }
            return null;
        }

        public bool SaveUpdatedEmployeeData<T>(Employee employeeToUpdate, T userInput, string propertyType)
        {
            // Getting the PropertyInfo object corresponding to the specified property type of the Employee class.
            PropertyInfo property = typeof(Employee).GetProperty(propertyType)!;
            if (property != null)
            {
                // assigning the value of the specified property of the employeeToUpdate object.
                property.SetValue(employeeToUpdate, userInput);

                // Update the employee in the repository with the modified data.
                bool isUpdated = _employeeRepo.UpdateEmployee(employeeToUpdate);
                return isUpdated;
            }
            else
                return false;
        }

        public List<(Department, List<Role>)>? GetDepartmentAndRolesData()
        {

            var departmentsAndRoles = _employeeRepo.LoadDepartmentsAndRoles();
            if (departmentsAndRoles != null)
            {
                return departmentsAndRoles;
            }
            else
            {
                return null;
            }
        }
        public List<Employee> GetEmployeeRecords()
        {
            return _employeeRepo.LoadEmployeeRecords();
        }

        public bool DeleteEmployee(string employeeNumber)
        {
            return _employeeRepo.RemoveEmployee(employeeNumber);
        }
    }
}
