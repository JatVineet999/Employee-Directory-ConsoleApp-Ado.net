using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Data.SqlClient;

namespace Infrastructure.Repos
{
    public class EmployeeRepo : CommonRepo, IEmployeeRepo
    {
        public EmployeeRepo(string connectionString) : base(connectionString)
        {
        }

        public List<Employee> LoadEmployeeRecords()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT 
                            e.*, 
                            d.DepartmentName,
                            r.RoleName
                        FROM 
                            Employee e
                        INNER JOIN 
                            Department d ON e.DepartmentID = d.DepartmentID
                        INNER JOIN 
                            Role r ON e.RoleID = r.RoleID";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Email = reader["Email"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                                Location = reader["Location"].ToString(),
                                DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                RoleID = Convert.ToInt32(reader["RoleID"]),
                                ManagerName = reader["ManagerName"].ToString(),
                                ProjectName = reader["ProjectName"].ToString(),
                                EmployeeNumber = reader["EmployeeID"].ToString()
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while loading employee records from the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }

            return employees;
        }

        public bool SaveEmployeeRecord(Employee employee)
        {
            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Employee (EmployeeID, FirstName, LastName, MobileNumber, Email, 
                        DateOfBirth, JoiningDate, Location, DepartmentID, RoleID, ManagerName, ProjectName)
                        VALUES (@EmployeeID, @FirstName, @LastName, @MobileNumber, @Email, @DateOfBirth, 
                        @JoiningDate, @Location, @DepartmentID, @RoleID, @ManagerName, @ProjectName)";

                    command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeNumber);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    command.Parameters.AddWithValue("@Location", employee.Location);
                    command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    command.Parameters.AddWithValue("@RoleID", employee.RoleID);
                    command.Parameters.AddWithValue("@ManagerName", employee.ManagerName);
                    command.Parameters.AddWithValue("@ProjectName", employee.ProjectName);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while saving the employee record to the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }
        }

        public bool RemoveEmployee(string employeeNumber)
        {
            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Employee WHERE EmployeeID = @EmployeeNumber";
                    command.Parameters.AddWithValue("@EmployeeNumber", employeeNumber);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while removing the employee from the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }
        }

        public bool UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                using (SqlConnection connection = CreateOpenConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Employee 
                        SET 
                            FirstName = @FirstName, 
                            LastName = @LastName, 
                            MobileNumber = @MobileNumber, 
                            Email = @Email, 
                            DateOfBirth = @DateOfBirth, 
                            JoiningDate = @JoiningDate, 
                            Location = @Location, 
                            DepartmentID = @DepartmentID, 
                            RoleID = @RoleID, 
                            ManagerName = @ManagerName, 
                            ProjectName = @ProjectName 
                        WHERE 
                            EmployeeID = @EmployeeID";

                    command.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                    command.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                    command.Parameters.AddWithValue("@MobileNumber", updatedEmployee.MobileNumber);
                    command.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                    command.Parameters.AddWithValue("@DateOfBirth", updatedEmployee.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", updatedEmployee.JoiningDate);
                    command.Parameters.AddWithValue("@Location", updatedEmployee.Location);
                    command.Parameters.AddWithValue("@DepartmentID", updatedEmployee.DepartmentID);
                    command.Parameters.AddWithValue("@RoleID", updatedEmployee.RoleID);
                    command.Parameters.AddWithValue("@ManagerName", updatedEmployee.ManagerName);
                    command.Parameters.AddWithValue("@ProjectName", updatedEmployee.ProjectName);
                    command.Parameters.AddWithValue("@EmployeeID", updatedEmployee.EmployeeNumber);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while updating the employee record in the database: " + ex.Message;
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
