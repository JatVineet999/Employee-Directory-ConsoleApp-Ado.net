using Infrastructure.Models;
using System.Data.SqlClient;


namespace Infrastructure.Interfaces
{
    public interface ICommonRepo
    {
        List<(Department, List<Role>)> LoadDepartmentsAndRoles();

    }
}

