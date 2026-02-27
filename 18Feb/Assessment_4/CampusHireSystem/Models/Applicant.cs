using System;

namespace CampusHireSystem.Models
{
    /// <summary>
    /// Applicant model class
    /// Serializable for file persistence
    /// </summary>
    [Serializable]
    public class Applicant
    {
        /// <summary>
        /// Applicant ID (Format: CHxxxxxx - 8 characters)
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Applicant Name (4-15 characters)
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// Current Location (Mumbai/Pune/Chennai)
        /// </summary>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Preferred Job Location (Mumbai/Pune/Chennai/Delhi/Kolkata/Bangalore)
        /// </summary>
        public string PreferredJobLocation { get; set; }

        /// <summary>
        /// Core Competency (.NET/JAVA/ORACLE/Testing)
        /// </summary>
        public string CoreCompetency { get; set; }

        /// <summary>
        /// Passing Year (Degree completion year)
        /// </summary>
        public int PassingYear { get; set; }

        /// <summary>
        /// Registration date
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Applicant()
        {
            RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Display applicant details
        /// </summary>
        public void DisplayDetails()
        {
            Console.WriteLine($"\n{"Applicant ID:",-25} {ApplicantId}");
            Console.WriteLine($"{"Name:",-25} {ApplicantName}");
            Console.WriteLine($"{"Current Location:",-25} {CurrentLocation}");
            Console.WriteLine($"{"Preferred Job Location:",-25} {PreferredJobLocation}");
            Console.WriteLine($"{"Core Competency:",-25} {CoreCompetency}");
            Console.WriteLine($"{"Passing Year:",-25} {PassingYear}");
            Console.WriteLine($"{"Registration Date:",-25} {RegistrationDate:dd-MMM-yyyy}");
        }

        /// <summary>
        /// Override ToString for display
        /// </summary>
        public override string ToString()
        {
            return $"{ApplicantId,-12} {ApplicantName,-20} {CurrentLocation,-12} {PreferredJobLocation,-12} {CoreCompetency,-12} {PassingYear,-12}";
        }
    }
}
