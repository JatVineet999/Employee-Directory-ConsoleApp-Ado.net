using Infrastructure.Models;
using Application.Interfaces;
using Presentation.Constants;
using Presentation.Interfaces;

namespace Presentation.MenuManagers
{
    class RolesMenuManager : IRolesMenuManager
    {
        private readonly IMainMenuManager _mainMenuManager;
        private readonly IDepartmentAndRolesServices _departmentAndRolesServices;
        private readonly Dictionary<RolesMenuOption, Action> _menuActions;

        public RolesMenuManager(IMainMenuManager mainMenuManager, IDepartmentAndRolesServices departmentAndRolesServices)

        {
            _departmentAndRolesServices = departmentAndRolesServices;
            _mainMenuManager = mainMenuManager;
            _menuActions = new Dictionary<RolesMenuOption, Action>
            {
                { RolesMenuOption.AddRole, AddRole },
                { RolesMenuOption.DisplayRoles, DisplayDepartmentsAndRoles },
                { RolesMenuOption.ReturnToMainMenu,_mainMenuManager.DisplayMainMenu}

            };
        }

        public void RolesMenuHandler()
        {
            while (true)
            {
                DisplayMenuOptions();
                if (!GetUserChoice(out RolesMenuOption selectedOption))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                if (_menuActions.ContainsKey(selectedOption))
                {
                    _menuActions[selectedOption].Invoke();
                }
                else
                {
                    Console.WriteLine("Invalid option selected. Please try again.");
                }

                if (!RequestMenuReturnSelection())
                {
                    return;
                }
            }
        }

        private void DisplayMenuOptions()
        {
            Console.WriteLine("--------------------------------------------------------------Roles Menu-------------------------------------------------------------------------");
            Console.WriteLine($"1.AddRole");
            Console.WriteLine($"2.DisplayRoles");
            Console.WriteLine($"Press '0' to Go Back to Menu");
        }

        private bool GetUserChoice(out RolesMenuOption selectedOption)
        {
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return Enum.TryParse(choice.ToString(), out selectedOption);
        }

        private bool RequestMenuReturnSelection()
        {
            Console.WriteLine("Press '0' to Go Back to Previous Menu options\n                 or\nPress any other key to return to Main Menu");
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (input == '0')
            {
                return true;
            }
            else
            {
                _mainMenuManager.DisplayMainMenu();
                return false;
            }
        }

        public int SelectDepartment()
        {

            var departmentsAndRoles = _departmentAndRolesServices.GetDepartmentsAndRoles();
            if (departmentsAndRoles != null)
            {
                Console.WriteLine("Available Departments:");
                for (int i = 0; i < departmentsAndRoles.Count; i++)
                {
                    var department = departmentsAndRoles[i].Item1;
                    Console.WriteLine($"{i + 1}. {department.DepartmentName}");
                }

                int departmentIndex = GetUserInput("Select a department by entering its number:", departmentsAndRoles.Count);

                return departmentsAndRoles[departmentIndex - 1].Item1.DepartmentID;
            }
            else
            {
                Console.WriteLine("No departments available.");
                return -1;
            }
        }


        public int SelectRoleinDepartment(int departmentId)
        {

            var departmentsAndRoles = _departmentAndRolesServices.GetDepartmentsAndRoles();
            if (departmentsAndRoles != null)
            {
                var department = departmentsAndRoles!.FirstOrDefault(d => d.Item1.DepartmentID == departmentId);
                Console.WriteLine($"Roles in Department ID {departmentId}:");
                var roles = department.Item2;
                for (int i = 0; i < roles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {roles[i].RoleName}");
                }

                int roleIndex = GetUserInput("Select a role by entering its number:", roles.Count);
                return roles[roleIndex - 1].RoleId;
            }
            else
            {
                Console.WriteLine("Failed to retrieve Data.");
                return -1;
            }

        }

        private int GetUserInput(string prompt, int maxInput)
        {
            int userInput;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out userInput) && userInput >= 1 && userInput <= maxInput)
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a valid number between 1 and {maxInput}.");
                }
            }
        }

        private void AddRole()
        {
            try
            {
                Console.WriteLine("Select Department in which you would like to add a new role:");
                int departmentId = SelectDepartment();

                if (departmentId == -1)
                {
                    Console.WriteLine("Invalid department selected. Please try again.");
                    return;
                }

                Console.Write("Enter the new role: ");
                string newRole = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(newRole))
                {
                    Console.WriteLine("Invalid role name. Role name cannot be empty.");
                    return;
                }

                if (_departmentAndRolesServices.AddRoleToDepartment(departmentId, newRole))
                {
                    Console.WriteLine($"Role '{newRole}' added to the department.");
                }
                else
                {
                    Console.WriteLine($"Failed to add role '{newRole}' to the department.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void DisplayDepartmentsAndRoles()
        {
            try
            {
                var departmentsAndRoles = _departmentAndRolesServices.GetDepartmentsAndRoles();
                if (departmentsAndRoles != null)
                {
                    Console.WriteLine("Available Departments and Roles:");
                    foreach (var departmentAndRoles in departmentsAndRoles)
                    {
                        var department = departmentAndRoles.Item1;
                        var roles = departmentAndRoles.Item2;

                        Console.WriteLine($"Department ID: {department.DepartmentID}, Name: {department.DepartmentName}");
                        Console.WriteLine("Roles:");
                        foreach (var role in roles)
                        {
                            Console.WriteLine($"- Role ID: {role.RoleId}, Name: {role.RoleName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve department and roles data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading departments and roles: " + ex.Message);
            }
        }



    }
}
