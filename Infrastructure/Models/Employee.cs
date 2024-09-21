namespace Infrastructure.Models
{
    public class Employee
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Location { get; set; }
        public int RoleID { get; set; } 
        public int DepartmentID { get; set; } 
        public string? ManagerName { get; set; }
        public string? ProjectName { get; set; }
        public string? EmployeeNumber { get; set; }
    }

}