using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CampusHireSystem.Models;

namespace CampusHireSystem.Services
{
    /// <summary>
    /// File service for serialization and deserialization
    /// </summary>
    public class FileService
    {
        private const string DataFileName = "applicants.dat";
        private readonly string _dataFilePath;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileService()
        {
            _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFileName);
        }

        /// <summary>
        /// Save applicants to file using binary serialization
        /// </summary>
        public void SaveApplicants(List<Applicant> applicants)
        {
            try
            {
#pragma warning disable SYSLIB0011 // BinaryFormatter is obsolete
                using (FileStream fs = new FileStream(_dataFilePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, applicants);
                }
#pragma warning restore SYSLIB0011

                Console.WriteLine($"✅ Data saved successfully to {DataFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving data: {ex.Message}");
            }
        }

        /// <summary>
        /// Load applicants from file using binary deserialization
        /// </summary>
        public List<Applicant> LoadApplicants()
        {
            try
            {
                if (!File.Exists(_dataFilePath))
                {
                    Console.WriteLine($"ℹ️  No existing data file found. Starting with empty database.");
                    return new List<Applicant>();
                }

#pragma warning disable SYSLIB0011 // BinaryFormatter is obsolete
                using (FileStream fs = new FileStream(_dataFilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (List<Applicant>)formatter.Deserialize(fs);
                }
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading data: {ex.Message}");
                return new List<Applicant>();
            }
        }

        /// <summary>
        /// Check if data file exists
        /// </summary>
        public bool DataFileExists()
        {
            return File.Exists(_dataFilePath);
        }

        /// <summary>
        /// Delete data file
        /// </summary>
        public bool DeleteDataFile()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    File.Delete(_dataFilePath);
                    Console.WriteLine($"✅ Data file deleted successfully");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error deleting data file: {ex.Message}");
                return false;
            }
        }
    }
}
