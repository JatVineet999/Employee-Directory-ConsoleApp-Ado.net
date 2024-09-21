using System;
using System.Collections.Generic;
using Presentation.Constants;
using Presentation.Interfaces;

namespace Presentation.MenuManagers
{
    class MainMenuManager : IMainMenuManager
    {
        private readonly Lazy<IEmployeesMenuManager> _employeesMenuManager;
        private readonly Lazy<IRolesMenuManager> _rolesMenuManager;

        public MainMenuManager(Lazy<IEmployeesMenuManager> employeesMenuManager, Lazy<IRolesMenuManager> rolesMenuManager)
        {
            _employeesMenuManager = employeesMenuManager ?? throw new ArgumentNullException(nameof(employeesMenuManager));
            _rolesMenuManager = rolesMenuManager ?? throw new ArgumentNullException(nameof(rolesMenuManager));
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("--------------------------------------------------------------Main Menu-------------------------------------------------------------------------");
            Console.WriteLine("1. Employees Menu");
            Console.WriteLine("2. Roles Menu");
            Console.WriteLine("3. Exit");

            string userInput = Console.ReadLine()!.Trim();
            if (Enum.TryParse(userInput, out MainMenuOption selectedOption))
            {
                MainMenuHandler(selectedOption);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
                DisplayMainMenu();
            }
        }

        private void MainMenuHandler(MainMenuOption option)
        {
            switch (option)
            {
                case MainMenuOption.EmployeesMenu:
                    _employeesMenuManager.Value.EmployeesMenuHandler();
                    break;
                case MainMenuOption.RolesMenu:
                    _rolesMenuManager.Value.RolesMenuHandler();
                    break;
                case MainMenuOption.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
