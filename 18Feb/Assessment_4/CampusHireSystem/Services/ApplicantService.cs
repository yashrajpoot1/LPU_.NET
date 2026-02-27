using System;
using System.Collections.Generic;
using System.Linq;
using CampusHireSystem.Models;
using CampusHireSystem.Utilities;

namespace CampusHireSystem.Services
{
    /// <summary>
    /// Service class to manage applicant operations
    /// </summary>
    public class ApplicantService
    {
        private List<Applicant> _applicants;
        private FileService _fileService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicantService()
        {
            _applicants = new List<Applicant>();
            _fileService = new FileService();
            LoadApplicants();
        }

        /// <summary>
        /// Add new applicant
        /// </summary>
        public bool AddApplicant(Applicant applicant)
        {
            // Validate applicant
            if (!ApplicantValidator.ValidateApplicant(applicant, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            // Check for duplicate ID
            if (_applicants.Any(a => a.ApplicantId.Equals(applicant.ApplicantId, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Applicant with ID {applicant.ApplicantId} already exists");
            }

            _applicants.Add(applicant);
            SaveApplicants();
            return true;
        }

        /// <summary>
        /// Get applicant by ID
        /// </summary>
        public Applicant GetApplicant(string applicantId)
        {
            return _applicants.FirstOrDefault(a => 
                a.ApplicantId.Equals(applicantId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get all applicants
        /// </summary>
        public List<Applicant> GetAllApplicants()
        {
            return _applicants.ToList();
        }

        /// <summary>
        /// Update applicant
        /// </summary>
        public bool UpdateApplicant(string applicantId, Applicant updatedApplicant)
        {
            var existingApplicant = GetApplicant(applicantId);
            if (existingApplicant == null)
            {
                return false;
            }

            // Validate updated applicant
            if (!ApplicantValidator.ValidateApplicant(updatedApplicant, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            // Update fields
            existingApplicant.ApplicantName = updatedApplicant.ApplicantName;
            existingApplicant.CurrentLocation = updatedApplicant.CurrentLocation;
            existingApplicant.PreferredJobLocation = updatedApplicant.PreferredJobLocation;
            existingApplicant.CoreCompetency = updatedApplicant.CoreCompetency;
            existingApplicant.PassingYear = updatedApplicant.PassingYear;

            SaveApplicants();
            return true;
        }

        /// <summary>
        /// Delete applicant
        /// </summary>
        public bool DeleteApplicant(string applicantId)
        {
            var applicant = GetApplicant(applicantId);
            if (applicant == null)
            {
                return false;
            }

            _applicants.Remove(applicant);
            SaveApplicants();
            return true;
        }

        /// <summary>
        /// Get applicants by location
        /// </summary>
        public List<Applicant> GetApplicantsByLocation(string location)
        {
            return _applicants.Where(a => 
                a.CurrentLocation.Equals(location, StringComparison.OrdinalIgnoreCase) ||
                a.PreferredJobLocation.Equals(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Get applicants by competency
        /// </summary>
        public List<Applicant> GetApplicantsByCompetency(string competency)
        {
            return _applicants.Where(a => 
                a.CoreCompetency.Equals(competency, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Get applicants by passing year
        /// </summary>
        public List<Applicant> GetApplicantsByPassingYear(int year)
        {
            return _applicants.Where(a => a.PassingYear == year).ToList();
        }

        /// <summary>
        /// Get total count
        /// </summary>
        public int GetTotalCount()
        {
            return _applicants.Count;
        }

        /// <summary>
        /// Save applicants to file
        /// </summary>
        private void SaveApplicants()
        {
            _fileService.SaveApplicants(_applicants);
        }

        /// <summary>
        /// Load applicants from file
        /// </summary>
        private void LoadApplicants()
        {
            var loadedApplicants = _fileService.LoadApplicants();
            if (loadedApplicants != null && loadedApplicants.Count > 0)
            {
                _applicants = loadedApplicants;
                Console.WriteLine($"✅ Loaded {_applicants.Count} applicant(s) from file");
            }
        }

        /// <summary>
        /// Generate statistics report
        /// </summary>
        public void GenerateStatistics()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     APPLICANT STATISTICS               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine($"\nTotal Applicants: {_applicants.Count}");

            // By Location
            Console.WriteLine("\nBy Current Location:");
            var locationGroups = _applicants.GroupBy(a => a.CurrentLocation);
            foreach (var group in locationGroups)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()}");
            }

            // By Competency
            Console.WriteLine("\nBy Core Competency:");
            var competencyGroups = _applicants.GroupBy(a => a.CoreCompetency);
            foreach (var group in competencyGroups)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()}");
            }

            // By Passing Year
            Console.WriteLine("\nBy Passing Year:");
            var yearGroups = _applicants.GroupBy(a => a.PassingYear).OrderByDescending(g => g.Key);
            foreach (var group in yearGroups)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()}");
            }
        }
    }
}
