using System;
using CampusHireSystem.Models;
using CampusHireSystem.Services;
using CampusHireSystem.Utilities;

namespace CampusHireSystem
{
    /// <summary>
    /// Main program for CampusHire Applicant Management System
    /// </summary>
    class Program
    {
        private static ApplicantService _applicantService = new ApplicantService();

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  CAMPUSHIRE APPLICANT MANAGEMENT       ║");
            Console.WriteLine("║         SYSTEM                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddApplicant();
                            break;
                        case "2":
                            DisplayAllApplicants();
                            break;
                        case "3":
                            SearchApplicant();
                            break;
                        case "4":
                            UpdateApplicant();
                            break;
                        case "5":
                            DeleteApplicant();
                            break;
                        case "6":
                            FilterApplicants();
                            break;
                        case "7":
                            _applicantService.GenerateStatistics();
                            break;
                        case "8":
                            Console.WriteLine("\nThank you for using CampusHire System!");
                            return;
                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n❌ Validation Error: {ex.Message}");
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
            Console.WriteLine("1. Add New Applicant");
            Console.WriteLine("2. Display All Applicants");
            Console.WriteLine("3. Search Applicant by ID");
            Console.WriteLine("4. Update Applicant Details");
            Console.WriteLine("5. Delete Applicant");
            Console.WriteLine("6. Filter Applicants");
            Console.WriteLine("7. View Statistics");
            Console.WriteLine("8. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddApplicant()
        {
            Console.WriteLine("\n--- Add New Applicant ---");

            Console.Write("Enter Applicant ID (Format: CHxxxxxx): ");
            string id = Console.ReadLine();

            Console.Write("Enter Applicant Name (4-15 characters): ");
            string name = Console.ReadLine();

            Console.WriteLine("\nCurrent Location Options:");
            Console.WriteLine("1. Mumbai");
            Console.WriteLine("2. Pune");
            Console.WriteLine("3. Chennai");
            Console.Write("Select Current Location (1-3): ");
            string currentLocation = GetLocationChoice(Console.ReadLine(), true);

            Console.WriteLine("\nPreferred Job Location Options:");
            Console.WriteLine("1. Mumbai");
            Console.WriteLine("2. Pune");
            Console.WriteLine("3. Chennai");
            Console.WriteLine("4. Delhi");
            Console.WriteLine("5. Kolkata");
            Console.WriteLine("6. Bangalore");
            Console.Write("Select Preferred Job Location (1-6): ");
            string preferredLocation = GetLocationChoice(Console.ReadLine(), false);

            Console.WriteLine("\nCore Competency Options:");
            Console.WriteLine("1. .NET");
            Console.WriteLine("2. JAVA");
            Console.WriteLine("3. ORACLE");
            Console.WriteLine("4. Testing");
            Console.Write("Select Core Competency (1-4): ");
            string competency = GetCompetencyChoice(Console.ReadLine());

            Console.Write($"Enter Passing Year (Max: {DateTime.Now.Year}): ");
            int passingYear = int.Parse(Console.ReadLine());

            var applicant = new Applicant
            {
                ApplicantId = id,
                ApplicantName = name,
                CurrentLocation = currentLocation,
                PreferredJobLocation = preferredLocation,
                CoreCompetency = competency,
                PassingYear = passingYear
            };

            if (_applicantService.AddApplicant(applicant))
            {
                Console.WriteLine("\n✅ Applicant added successfully!");
                applicant.DisplayDetails();
            }
        }

        static void DisplayAllApplicants()
        {
            var applicants = _applicantService.GetAllApplicants();

            if (applicants.Count == 0)
            {
                Console.WriteLine("\n❌ No applicants found in the system!");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     ALL APPLICANTS                     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"\nTotal Applicants: {applicants.Count}\n");
            Console.WriteLine($"{"ID",-12} {"Name",-20} {"Current",-12} {"Preferred",-12} {"Competency",-12} {"Year",-12}");
            Console.WriteLine(new string('-', 90));

            foreach (var applicant in applicants)
            {
                Console.WriteLine(applicant.ToString());
            }
        }

        static void SearchApplicant()
        {
            Console.Write("\nEnter Applicant ID to search: ");
            string id = Console.ReadLine();

            var applicant = _applicantService.GetApplicant(id);
            if (applicant != null)
            {
                Console.WriteLine("\n✅ Applicant Found:");
                applicant.DisplayDetails();
            }
            else
            {
                Console.WriteLine($"\n❌ Applicant with ID '{id}' not found!");
            }
        }

        static void UpdateApplicant()
        {
            Console.Write("\nEnter Applicant ID to update: ");
            string id = Console.ReadLine();

            var existingApplicant = _applicantService.GetApplicant(id);
            if (existingApplicant == null)
            {
                Console.WriteLine($"\n❌ Applicant with ID '{id}' not found!");
                return;
            }

            Console.WriteLine("\n--- Current Details ---");
            existingApplicant.DisplayDetails();

            Console.WriteLine("\n--- Enter New Details ---");

            Console.Write($"Enter Applicant Name [{existingApplicant.ApplicantName}]: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                name = existingApplicant.ApplicantName;

            Console.WriteLine("\nCurrent Location Options:");
            Console.WriteLine("1. Mumbai");
            Console.WriteLine("2. Pune");
            Console.WriteLine("3. Chennai");
            Console.Write($"Select Current Location [{existingApplicant.CurrentLocation}]: ");
            string currentLocationInput = Console.ReadLine();
            string currentLocation = string.IsNullOrWhiteSpace(currentLocationInput) 
                ? existingApplicant.CurrentLocation 
                : GetLocationChoice(currentLocationInput, true);

            Console.WriteLine("\nPreferred Job Location Options:");
            Console.WriteLine("1. Mumbai  2. Pune  3. Chennai  4. Delhi  5. Kolkata  6. Bangalore");
            Console.Write($"Select Preferred Job Location [{existingApplicant.PreferredJobLocation}]: ");
            string preferredLocationInput = Console.ReadLine();
            string preferredLocation = string.IsNullOrWhiteSpace(preferredLocationInput) 
                ? existingApplicant.PreferredJobLocation 
                : GetLocationChoice(preferredLocationInput, false);

            Console.WriteLine("\nCore Competency Options:");
            Console.WriteLine("1. .NET  2. JAVA  3. ORACLE  4. Testing");
            Console.Write($"Select Core Competency [{existingApplicant.CoreCompetency}]: ");
            string competencyInput = Console.ReadLine();
            string competency = string.IsNullOrWhiteSpace(competencyInput) 
                ? existingApplicant.CoreCompetency 
                : GetCompetencyChoice(competencyInput);

            Console.Write($"Enter Passing Year [{existingApplicant.PassingYear}]: ");
            string yearInput = Console.ReadLine();
            int passingYear = string.IsNullOrWhiteSpace(yearInput) 
                ? existingApplicant.PassingYear 
                : int.Parse(yearInput);

            var updatedApplicant = new Applicant
            {
                ApplicantId = id,
                ApplicantName = name,
                CurrentLocation = currentLocation,
                PreferredJobLocation = preferredLocation,
                CoreCompetency = competency,
                PassingYear = passingYear
            };

            if (_applicantService.UpdateApplicant(id, updatedApplicant))
            {
                Console.WriteLine("\n✅ Applicant updated successfully!");
                var updated = _applicantService.GetApplicant(id);
                updated.DisplayDetails();
            }
        }

        static void DeleteApplicant()
        {
            Console.Write("\nEnter Applicant ID to delete: ");
            string id = Console.ReadLine();

            var applicant = _applicantService.GetApplicant(id);
            if (applicant == null)
            {
                Console.WriteLine($"\n❌ Applicant with ID '{id}' not found!");
                return;
            }

            Console.WriteLine("\n--- Applicant to Delete ---");
            applicant.DisplayDetails();

            Console.Write("\nAre you sure you want to delete this applicant? (y/n): ");
            string confirm = Console.ReadLine();

            if (confirm.ToLower() == "y")
            {
                if (_applicantService.DeleteApplicant(id))
                {
                    Console.WriteLine("\n✅ Applicant deleted successfully!");
                }
            }
            else
            {
                Console.WriteLine("\n❌ Deletion cancelled.");
            }
        }

        static void FilterApplicants()
        {
            Console.WriteLine("\n--- Filter Applicants ---");
            Console.WriteLine("1. By Current Location");
            Console.WriteLine("2. By Core Competency");
            Console.WriteLine("3. By Passing Year");
            Console.Write("\nSelect filter option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FilterByLocation();
                    break;
                case "2":
                    FilterByCompetency();
                    break;
                case "3":
                    FilterByPassingYear();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        static void FilterByLocation()
        {
            Console.WriteLine("\nLocations:");
            Console.WriteLine("1. Mumbai  2. Pune  3. Chennai  4. Delhi  5. Kolkata  6. Bangalore");
            Console.Write("Select location: ");
            string location = GetLocationChoice(Console.ReadLine(), false);

            var applicants = _applicantService.GetApplicantsByLocation(location);

            if (applicants.Count == 0)
            {
                Console.WriteLine($"\n❌ No applicants found for location '{location}'");
                return;
            }

            Console.WriteLine($"\n--- Applicants for {location} ---");
            Console.WriteLine($"{"ID",-12} {"Name",-20} {"Current",-12} {"Preferred",-12} {"Competency",-12}");
            Console.WriteLine(new string('-', 75));

            foreach (var applicant in applicants)
            {
                Console.WriteLine($"{applicant.ApplicantId,-12} {applicant.ApplicantName,-20} {applicant.CurrentLocation,-12} {applicant.PreferredJobLocation,-12} {applicant.CoreCompetency,-12}");
            }
        }

        static void FilterByCompetency()
        {
            Console.WriteLine("\nCompetencies:");
            Console.WriteLine("1. .NET  2. JAVA  3. ORACLE  4. Testing");
            Console.Write("Select competency: ");
            string competency = GetCompetencyChoice(Console.ReadLine());

            var applicants = _applicantService.GetApplicantsByCompetency(competency);

            if (applicants.Count == 0)
            {
                Console.WriteLine($"\n❌ No applicants found for competency '{competency}'");
                return;
            }

            Console.WriteLine($"\n--- Applicants with {competency} Competency ---");
            Console.WriteLine($"{"ID",-12} {"Name",-20} {"Location",-12} {"Year",-12}");
            Console.WriteLine(new string('-', 60));

            foreach (var applicant in applicants)
            {
                Console.WriteLine($"{applicant.ApplicantId,-12} {applicant.ApplicantName,-20} {applicant.CurrentLocation,-12} {applicant.PassingYear,-12}");
            }
        }

        static void FilterByPassingYear()
        {
            Console.Write("\nEnter Passing Year: ");
            int year = int.Parse(Console.ReadLine());

            var applicants = _applicantService.GetApplicantsByPassingYear(year);

            if (applicants.Count == 0)
            {
                Console.WriteLine($"\n❌ No applicants found for passing year {year}");
                return;
            }

            Console.WriteLine($"\n--- Applicants Passed in {year} ---");
            Console.WriteLine($"{"ID",-12} {"Name",-20} {"Competency",-12} {"Location",-12}");
            Console.WriteLine(new string('-', 60));

            foreach (var applicant in applicants)
            {
                Console.WriteLine($"{applicant.ApplicantId,-12} {applicant.ApplicantName,-20} {applicant.CoreCompetency,-12} {applicant.CurrentLocation,-12}");
            }
        }

        static string GetLocationChoice(string choice, bool isCurrentLocation)
        {
            var locations = isCurrentLocation 
                ? ApplicantValidator.ValidCurrentLocations 
                : ApplicantValidator.ValidPreferredLocations;

            switch (choice)
            {
                case "1": return "Mumbai";
                case "2": return "Pune";
                case "3": return "Chennai";
                case "4": return isCurrentLocation ? null : "Delhi";
                case "5": return isCurrentLocation ? null : "Kolkata";
                case "6": return isCurrentLocation ? null : "Bangalore";
                default:
                    throw new ArgumentException("Invalid location choice");
            }
        }

        static string GetCompetencyChoice(string choice)
        {
            switch (choice)
            {
                case "1": return ".NET";
                case "2": return "JAVA";
                case "3": return "ORACLE";
                case "4": return "Testing";
                default:
                    throw new ArgumentException("Invalid competency choice");
            }
        }
    }
}
