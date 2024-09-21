using Infrastructure.Interfaces;
using System.Data.SqlClient;

namespace Infrastructure.Repos
{
    public class DepartmentsRepo : CommonRepo, IDepartmentsRepo
    {
        public DepartmentsRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public bool SaveNewRole(int departmentId, string newRole)
        {
            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Role (RoleName, DepartmentID) VALUES (@RoleName, @DepartmentID)";
                    command.Parameters.AddWithValue("@RoleName", newRole);
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while saving the new role to the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
