using System;
using System.Collections.Generic;
using System.Linq;
using CampusHireSystem.Models;

namespace CampusHireSystem.Utilities
{
    /// <summary>
    /// Validator class for applicant data
    /// </summary>
    public static class ApplicantValidator
    {
        // Valid location options
        public static readonly List<string> ValidCurrentLocations = new List<string> 
        { 
            "Mumbai", "Pune", "Chennai" 
        };

        public static readonly List<string> ValidPreferredLocations = new List<string> 
        { 
            "Mumbai", "Pune", "Chennai", "Delhi", "Kolkata", "Bangalore" 
        };

        public static readonly List<string> ValidCompetencies = new List<string> 
        { 
            ".NET", "JAVA", "ORACLE", "Testing" 
        };

        /// <summary>
        /// Validate applicant ID
        /// Rules: Exactly 8 characters, must start with "CH"
        /// </summary>
        public static bool ValidateApplicantId(string applicantId, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(applicantId))
            {
                errorMessage = "Applicant ID cannot be empty";
                return false;
            }

            if (applicantId.Length != 8)
            {
                errorMessage = "Applicant ID must be exactly 8 characters long";
                return false;
            }

            if (!applicantId.StartsWith("CH", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Applicant ID must start with 'CH'";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate applicant name
        /// Rules: 4-15 characters
        /// </summary>
        public static bool ValidateApplicantName(string name, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "Applicant Name cannot be empty";
                return false;
            }

            if (name.Length < 4)
            {
                errorMessage = "Applicant Name must be at least 4 characters long";
                return false;
            }

            if (name.Length > 15)
            {
                errorMessage = "Applicant Name cannot exceed 15 characters";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate current location
        /// </summary>
        public static bool ValidateCurrentLocation(string location, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(location))
            {
                errorMessage = "Current Location cannot be empty";
                return false;
            }

            if (!ValidCurrentLocations.Contains(location, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Current Location must be one of: {string.Join(", ", ValidCurrentLocations)}";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate preferred job location
        /// </summary>
        public static bool ValidatePreferredLocation(string location, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(location))
            {
                errorMessage = "Preferred Job Location cannot be empty";
                return false;
            }

            if (!ValidPreferredLocations.Contains(location, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Preferred Job Location must be one of: {string.Join(", ", ValidPreferredLocations)}";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate core competency
        /// </summary>
        public static bool ValidateCoreCompetency(string competency, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(competency))
            {
                errorMessage = "Core Competency cannot be empty";
                return false;
            }

            if (!ValidCompetencies.Contains(competency, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Core Competency must be one of: {string.Join(", ", ValidCompetencies)}";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate passing year
        /// Rules: Cannot be greater than current year
        /// </summary>
        public static bool ValidatePassingYear(int year, out string errorMessage)
        {
            errorMessage = null;

            if (year <= 0)
            {
                errorMessage = "Passing Year must be a valid year";
                return false;
            }

            if (year > DateTime.Now.Year)
            {
                errorMessage = $"Passing Year cannot be greater than current year ({DateTime.Now.Year})";
                return false;
            }

            if (year < 1950)
            {
                errorMessage = "Passing Year must be after 1950";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate complete applicant
        /// </summary>
        public static bool ValidateApplicant(Applicant applicant, out string errorMessage)
        {
            errorMessage = null;

            if (applicant == null)
            {
                errorMessage = "Applicant cannot be null";
                return false;
            }

            // Validate all fields
            if (!ValidateApplicantId(applicant.ApplicantId, out errorMessage))
                return false;

            if (!ValidateApplicantName(applicant.ApplicantName, out errorMessage))
                return false;

            if (!ValidateCurrentLocation(applicant.CurrentLocation, out errorMessage))
                return false;

            if (!ValidatePreferredLocation(applicant.PreferredJobLocation, out errorMessage))
                return false;

            if (!ValidateCoreCompetency(applicant.CoreCompetency, out errorMessage))
                return false;

            if (!ValidatePassingYear(applicant.PassingYear, out errorMessage))
                return false;

            return true;
        }
    }
}
