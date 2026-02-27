using System;
using System.Collections.Generic;
using System.Linq;
using HealthSyncBilling.Models;

namespace HealthSyncBilling.Services
{
    /// <summary>
    /// Service class to manage billing operations
    /// </summary>
    public class BillingService
    {
        private List<Consultant> _consultants;

        /// <summary>
        /// Constructor
        /// </summary>
        public BillingService()
        {
            _consultants = new List<Consultant>();
        }

        /// <summary>
        /// Add consultant to the system
        /// </summary>
        /// <param name="consultant">Consultant to add</param>
        /// <returns>True if added successfully</returns>
        public bool AddConsultant(Consultant consultant)
        {
            // Validate consultant ID
            if (!Consultant.ValidateConsultantId(consultant.ConsultantId))
            {
                throw new ArgumentException("Invalid doctor id");
            }

            // Check for duplicate ID
            if (_consultants.Any(c => c.ConsultantId == consultant.ConsultantId))
            {
                throw new ArgumentException($"Consultant with ID {consultant.ConsultantId} already exists");
            }

            _consultants.Add(consultant);
            return true;
        }

        /// <summary>
        /// Get consultant by ID
        /// </summary>
        /// <param name="consultantId">Consultant ID</param>
        /// <returns>Consultant if found, null otherwise</returns>
        public Consultant GetConsultant(string consultantId)
        {
            return _consultants.FirstOrDefault(c => c.ConsultantId == consultantId);
        }

        /// <summary>
        /// Get all consultants
        /// </summary>
        /// <returns>List of all consultants</returns>
        public List<Consultant> GetAllConsultants()
        {
            return _consultants.ToList();
        }

        /// <summary>
        /// Get total payout for all consultants
        /// </summary>
        /// <returns>Total payout amount</returns>
        public decimal GetTotalPayout()
        {
            return _consultants.Sum(c => c.CalculateNetPayout());
        }

        /// <summary>
        /// Get total TDS collected
        /// </summary>
        /// <returns>Total TDS amount</returns>
        public decimal GetTotalTDS()
        {
            return _consultants.Sum(c => 
            {
                decimal gross = c.CalculateGrossPayout();
                decimal tdsPercentage = c.CalculateTDS(gross);
                return gross * (tdsPercentage / 100);
            });
        }

        /// <summary>
        /// Generate summary report
        /// </summary>
        public void GenerateSummaryReport()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     HEALTHSYNC BILLING SUMMARY         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"\nTotal Consultants: {_consultants.Count}");
            Console.WriteLine($"In-House Consultants: {_consultants.OfType<InHouseConsultant>().Count()}");
            Console.WriteLine($"Visiting Consultants: {_consultants.OfType<VisitingConsultant>().Count()}");
            Console.WriteLine($"\nTotal Gross Payout: {_consultants.Sum(c => c.CalculateGrossPayout()):C}");
            Console.WriteLine($"Total TDS Collected: {GetTotalTDS():C}");
            Console.WriteLine($"Total Net Payout: {GetTotalPayout():C}");
        }

        /// <summary>
        /// Generate detailed report for all consultants
        /// </summary>
        public void GenerateDetailedReport()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     DETAILED CONSULTANT REPORT         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine($"\n{"ID",-10} {"Name",-20} {"Type",-15} {"Gross",-12} {"TDS%",-8} {"Net",-12}");
            Console.WriteLine(new string('-', 85));

            foreach (var consultant in _consultants)
            {
                string type = consultant is InHouseConsultant ? "In-House" : "Visiting";
                decimal gross = consultant.CalculateGrossPayout();
                decimal tdsPercentage = consultant.GetTDSPercentage();
                decimal net = consultant.CalculateNetPayout();

                Console.WriteLine($"{consultant.ConsultantId,-10} {consultant.Name,-20} {type,-15} {gross,-12:C} {tdsPercentage,-8}% {net,-12:C}");
            }
        }

        /// <summary>
        /// Get consultants by type
        /// </summary>
        /// <typeparam name="T">Consultant type</typeparam>
        /// <returns>List of consultants of specified type</returns>
        public List<T> GetConsultantsByType<T>() where T : Consultant
        {
            return _consultants.OfType<T>().ToList();
        }

        /// <summary>
        /// Get high earners (gross payout > threshold)
        /// </summary>
        /// <param name="threshold">Threshold amount</param>
        /// <returns>List of high earning consultants</returns>
        public List<Consultant> GetHighEarners(decimal threshold)
        {
            return _consultants.Where(c => c.CalculateGrossPayout() > threshold).ToList();
        }
    }
}
