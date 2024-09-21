using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IDepartmentsRepo : ICommonRepo
    {
        bool SaveNewRole(int departmentId, string newRole);
    }
}
