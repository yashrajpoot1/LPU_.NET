using System;

namespace MediSureClinic
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        public static PatientBill LastBill;
        public static bool HasLastBill = false;

        public static void CreateNewBill()
        {
            Console.WriteLine("Enter Bill Id:");
            string billId = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(billId))
            {
                Console.WriteLine("Bill ID cannot be empty. Please try again.");
                return;
            }

            Console.WriteLine("Enter Patient Name:");
            string patientName = Console.ReadLine();

            Console.WriteLine("Is the patient insured? (Y/N):");
            string insuranceInput = Console.ReadLine();
            bool hasInsurance = insuranceInput?.ToUpper() == "Y";

            Console.WriteLine("Enter Consultation Fee:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consultationFee) || consultationFee <= 0)
            {
                Console.WriteLine("Consultation Fee must be greater than 0. Please try again.");
                return;
            }

            Console.WriteLine("Enter Lab Charges:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal labCharges) || labCharges < 0)
            {
                Console.WriteLine("Lab Charges must be 0 or greater. Please try again.");
                return;
            }

            Console.WriteLine("Enter Medicine Charges:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal medicineCharges) || medicineCharges < 0)
            {
                Console.WriteLine("Medicine Charges must be 0 or greater. Please try again.");
                return;
            }

            // Calculate billing amounts
            decimal grossAmount = consultationFee + labCharges + medicineCharges;
            decimal discountAmount = hasInsurance ? grossAmount * 0.10m : 0;
            decimal finalPayable = grossAmount - discountAmount;

            // Create and store the bill
            LastBill = new PatientBill
            {
                BillId = billId,
                PatientName = patientName,
                HasInsurance = hasInsurance,
                ConsultationFee = consultationFee,
                LabCharges = labCharges,
                MedicineCharges = medicineCharges,
                GrossAmount = grossAmount,
                DiscountAmount = discountAmount,
                FinalPayable = finalPayable
            };

            HasLastBill = true;

            Console.WriteLine("Bill created successfully.");
            Console.WriteLine($"Gross Amount: {grossAmount:F2}");
            Console.WriteLine($"Discount Amount: {discountAmount:F2}");
            Console.WriteLine($"Final Payable: {finalPayable:F2}");
        }

        public static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("----------- Last Bill -----------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                // Display menu
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your option:");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        PatientBill.CreateNewBill();
                        break;
                    case "2":
                        PatientBill.ViewLastBill();
                        break;
                    case "3":
                        PatientBill.ClearLastBill();
                        break;
                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid menu option (1-4).");
                        break;
                }

                if (!exitProgram)
                {
                    Console.WriteLine("------------------------------------------------------------");
                }
            }
        }
    }
}