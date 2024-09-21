using Infrastructure.Models;

namespace Presentation.Interfaces
{
    interface IRolesMenuManager
    {
        void RolesMenuHandler();
        int SelectDepartment();
        int SelectRoleinDepartment(int departmentId);
    }
}
