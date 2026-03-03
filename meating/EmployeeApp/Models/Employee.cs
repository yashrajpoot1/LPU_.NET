using System;

namespace EmployeeApp.Models
{
    /// <summary>
    /// Employee class with required fields
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Job Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Date of Joining
        /// </summary>
        public DateTime DOJ { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Employee()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public Employee(int employeeID, string firstName, string lastName, string title, DateTime dob, DateTime doj, string city)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            DOB = dob;
            DOJ = doj;
            City = city;
        }

        /// <summary>
        /// Display employee details
        /// </summary>
        public void DisplayDetails()
        {
            Console.WriteLine($"\n{"Employee ID:",-20} {EmployeeID}");
            Console.WriteLine($"{"Name:",-20} {FirstName} {LastName}");
            Console.WriteLine($"{"Title:",-20} {Title}");
            Console.WriteLine($"{"Date of Birth:",-20} {DOB:dd-MMM-yyyy}");
            Console.WriteLine($"{"Date of Joining:",-20} {DOJ:dd-MMM-yyyy}");
            Console.WriteLine($"{"City:",-20} {City}");
            Console.WriteLine($"{"Age:",-20} {CalculateAge()} years");
            Console.WriteLine($"{"Experience:",-20} {CalculateExperience()} years");
        }

        /// <summary>
        /// Calculate age from DOB
        /// </summary>
        public int CalculateAge()
        {
            int age = DateTime.Now.Year - DOB.Year;
            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                age--;
            return age;
        }

        /// <summary>
        /// Calculate work experience from DOJ
        /// </summary>
        public int CalculateExperience()
        {
            int experience = DateTime.Now.Year - DOJ.Year;
            if (DateTime.Now.DayOfYear < DOJ.DayOfYear)
                experience--;
            return experience;
        }

        /// <summary>
        /// Get full name
        /// </summary>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Override ToString
        /// </summary>
        public override string ToString()
        {
            return $"{EmployeeID,-10} {FirstName,-15} {LastName,-15} {Title,-20} {City,-15}";
        }
    }
}
