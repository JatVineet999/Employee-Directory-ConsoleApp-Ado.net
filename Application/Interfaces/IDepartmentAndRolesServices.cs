using Infrastructure.Models;

namespace Application.Interfaces
{
    public interface IDepartmentAndRolesServices
    {
        bool AddRoleToDepartment(int departmentId, string newRole);
        List<(Department, List<Role>)>? GetDepartmentsAndRoles();
    }
}
