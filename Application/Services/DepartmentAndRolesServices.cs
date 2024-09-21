using Infrastructure.Models;
using Application.Interfaces;
using Infrastructure.Interfaces;
namespace Application.Services
{
    public class DepartmentAndRolesServices : IDepartmentAndRolesServices
    {
        private readonly IDepartmentsRepo _departmentsRepo;
        public DepartmentAndRolesServices(IDepartmentsRepo departmentsRepo)
        {
            _departmentsRepo = departmentsRepo;
        }
       
        public bool AddRoleToDepartment(int departmentId, string newRole)
        {
            return _departmentsRepo.SaveNewRole(departmentId, newRole);

        }

        public List<(Department, List<Role>)>? GetDepartmentsAndRoles()
        {
           
                var departmentsAndRoles = _departmentsRepo.LoadDepartmentsAndRoles();
                if (departmentsAndRoles != null)
                {
                    return departmentsAndRoles;
                }
                else
                {
                    return null;
                }
        }


    }



}