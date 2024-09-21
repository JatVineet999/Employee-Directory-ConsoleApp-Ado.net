using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repos
{
    public class CommonRepo : ICommonRepo
    {
        internal string _connectionString;

        public CommonRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal SqlConnection CreateOpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public List<(Department, List<Role>)> LoadDepartmentsAndRoles()
        {
            var departments = new Dictionary<int, Department>();
            var departmentRoles = new Dictionary<int, List<Role>>();

            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                {
                    string query = @"
                SELECT 
                    d.DepartmentId,
                    d.DepartmentName,
                    r.RoleId,
                    r.RoleName
                FROM 
                    Department d
                LEFT JOIN 
                    Role r ON d.DepartmentId = r.DepartmentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int departmentId = Convert.ToInt32(reader["DepartmentId"]);
                                string departmentName = reader["DepartmentName"].ToString()!;

                                if (!departments.ContainsKey(departmentId))
                                {
                                    departments[departmentId] = new Department
                                    {
                                        DepartmentID = departmentId,
                                        DepartmentName = departmentName
                                    };
                                    departmentRoles[departmentId] = new List<Role>();
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("RoleId")))
                                {
                                    int roleId = Convert.ToInt32(reader["RoleId"]);
                                    string roleName = reader["RoleName"].ToString()!;

                                    Role role = new Role
                                    {
                                        RoleId = roleId,
                                        RoleName = roleName,
                                        DepartmentID = departmentId
                                    };
                                    departmentRoles[departmentId].Add(role);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while loading departments and roles from the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }

            return departments.Select(d => (d.Value, departmentRoles[d.Key])).ToList();
        }

    }
}
