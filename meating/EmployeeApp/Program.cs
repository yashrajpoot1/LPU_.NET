using System;
using System.Collections.Generic;
using EmployeeApp.Models;

namespace EmployeeApp
{
    /// <summary>
    /// Main program for Employee Management
    /// </summary>
    class Program
    {
        private static List<Employee> employees = new List<Employee>();

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   EMPLOYEE MANAGEMENT SYSTEM           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            // Load sample data
            LoadSampleData();

            while (true)
            {
                try
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddEmployee();
                            break;
                        case "2":
                            DisplayAllEmployees();
                            break;
                        case "3":
                            SearchEmployee();
                            break;
                        case "4":
                            DisplayEmployeeDetails();
                            break;
                        case "5":
                            DisplayStatistics();
                            break;
                        case "6":
                            Console.WriteLine("\nThank you for using Employee Management System!");
                            return;
                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║           MAIN MENU                    ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("1. Add New Employee");
            Console.WriteLine("2. Display All Employees");
            Console.WriteLine("3. Search Employee by ID");
            Console.WriteLine("4. Display Employee Details");
            Console.WriteLine("5. Display Statistics");
            Console.WriteLine("6. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddEmployee()
        {
            Console.WriteLine("\n--- Add New Employee ---");

            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            // Check for duplicate ID
            if (employees.Exists(e => e.EmployeeID == employeeID))
            {
                Console.WriteLine($"\n❌ Employee with ID {employeeID} already exists!");
                return;
            }

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Date of Birth (dd-MM-yyyy): ");
            DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);

            Console.Write("Enter Date of Joining (dd-MM-yyyy): ");
            DateTime doj = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            var employee = new Employee(employeeID, firstName, lastName, title, dob, doj, city);
            employees.Add(employee);

            Console.WriteLine("\n✅ Employee added successfully!");
            employee.DisplayDetails();
        }

        static void DisplayAllEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\n❌ No employees found in the system!");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     ALL EMPLOYEES                      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"\nTotal Employees: {employees.Count}\n");
            Console.WriteLine($"{"ID",-10} {"First Name",-15} {"Last Name",-15} {"Title",-20} {"City",-15}");
            Console.WriteLine(new string('-', 80));

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }

        static void SearchEmployee()
        {
            Console.Write("\nEnter Employee ID to search: ");
            int id = int.Parse(Console.ReadLine());

            var employee = employees.Find(e => e.EmployeeID == id);
            if (employee != null)
            {
                Console.WriteLine("\n✅ Employee Found:");
                employee.DisplayDetails();
            }
            else
            {
                Console.WriteLine($"\n❌ Employee with ID {id} not found!");
            }
        }

        static void DisplayEmployeeDetails()
        {
            Console.Write("\nEnter Employee ID: ");
            int id = int.Parse(Console.ReadLine());

            var employee = employees.Find(e => e.EmployeeID == id);
            if (employee != null)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║     EMPLOYEE DETAILS                   ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                employee.DisplayDetails();
            }
            else
            {
                Console.WriteLine($"\n❌ Employee with ID {id} not found!");
            }
        }

        static void DisplayStatistics()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("\n❌ No employees in the system!");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     EMPLOYEE STATISTICS                ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine($"\nTotal Employees: {employees.Count}");

            // Average age
            double avgAge = 0;
            foreach (var emp in employees)
            {
                avgAge += emp.CalculateAge();
            }
            avgAge /= employees.Count;
            Console.WriteLine($"Average Age: {avgAge:F1} years");

            // Average experience
            double avgExp = 0;
            foreach (var emp in employees)
            {
                avgExp += emp.CalculateExperience();
            }
            avgExp /= employees.Count;
            Console.WriteLine($"Average Experience: {avgExp:F1} years");

            // Employees by city
            Console.WriteLine("\nEmployees by City:");
            var cityGroups = new Dictionary<string, int>();
            foreach (var emp in employees)
            {
                if (cityGroups.ContainsKey(emp.City))
                    cityGroups[emp.City]++;
                else
                    cityGroups[emp.City] = 1;
            }

            foreach (var city in cityGroups)
            {
                Console.WriteLine($"  {city.Key}: {city.Value}");
            }

            // Employees by title
            Console.WriteLine("\nEmployees by Title:");
            var titleGroups = new Dictionary<string, int>();
            foreach (var emp in employees)
            {
                if (titleGroups.ContainsKey(emp.Title))
                    titleGroups[emp.Title]++;
                else
                    titleGroups[emp.Title] = 1;
            }

            foreach (var title in titleGroups)
            {
                Console.WriteLine($"  {title.Key}: {title.Value}");
            }
        }

        static void LoadSampleData()
        {
            employees.Add(new Employee(1, "Rajesh", "Kumar", "Software Engineer", new DateTime(1990, 5, 15), new DateTime(2015, 7, 1), "Mumbai"));
            employees.Add(new Employee(2, "Priya", "Sharma", "Senior Developer", new DateTime(1988, 8, 20), new DateTime(2012, 3, 15), "Pune"));
            employees.Add(new Employee(3, "Amit", "Patel", "Team Lead", new DateTime(1985, 12, 10), new DateTime(2010, 1, 20), "Bangalore"));
            employees.Add(new Employee(4, "Sneha", "Singh", "Project Manager", new DateTime(1987, 3, 25), new DateTime(2011, 6, 10), "Delhi"));
            employees.Add(new Employee(5, "Vikram", "Verma", "Software Engineer", new DateTime(1992, 7, 8), new DateTime(2018, 9, 5), "Mumbai"));

            Console.WriteLine($"✅ Loaded {employees.Count} sample employees\n");
        }
    }
}
