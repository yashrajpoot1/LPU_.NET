using System;
using System.Collections.Generic;

class LibraryFineCalculator
{
    static void Main()
    {
        Console.WriteLine("=== LIBRARY FINE CALCULATOR ===\n");
        
        // Sample data: ItemType, DaysLate, UserType
        List<(char, int, char)> lateItems = new List<(char, int, char)>
        {
            ('B', 5, 'R'),   // Book, 5 days, Regular
            ('B', 5, 'S'),   // Book, 5 days, Student
            ('D', 2, 'R'),   // DVD, 2 days (grace period)
            ('D', 10, 'R'),  // DVD, 10 days
            ('J', 50, 'R'),  // Journal, 50 days (max cap)
            ('J', 7, 'S')    // Journal, 7 days, Student
        };
        
        foreach (var item in lateItems)
        {
            CalculateAndDisplayFine(item.Item1, item.Item2, item.Item3);
            Console.WriteLine("------------------------");
        }
    }
    
    static void CalculateAndDisplayFine(char itemType, int daysLate, char userType)
    {
        string itemName = GetItemName(itemType);
        string userTypeName = GetUserTypeName(userType);
        double fineRate = GetFineRate(itemType);
        double fine = CalculateFine(itemType, daysLate, userType);
        
        Console.WriteLine($"Item Type: {itemName}");
        Console.WriteLine($"User Type: {userTypeName}");
        Console.WriteLine($"Days Late: {daysLate}");
        Console.WriteLine($"Daily Fine Rate: ${fineRate:F2}");
        
        if (fine == 0)
        {
            Console.WriteLine("No fine - within grace period");
        }
        else
        {
            Console.WriteLine($"Calculated Fine: ${fine:F2}");
        }
    }
    
    static double CalculateFine(char itemType, int daysLate, char userType)
    {
        // Check grace period
        if (daysLate <= 3)
            return 0.0;
            
        // Calculate base fine
        double dailyRate = GetFineRate(itemType);
        double fine = (daysLate - 3) * dailyRate;
        
        // Apply maximum cap
        double maxFine = 20.00;
        if (fine > maxFine)
            fine = maxFine;
        
        // Apply student discount
        if (userType == 'S')
            fine *= 0.50;
        
        return Math.Round(fine, 2);
    }
    
    static double GetFineRate(char itemType)
    {
        return itemType switch
        {
            'B' => 0.50,  // Book
            'D' => 1.00,  // DVD
            'J' => 0.25,  // Journal
            _ => 0.00
        };
    }
    
    static string GetItemName(char itemType)
    {
        return itemType switch
        {
            'B' => "Book",
            'D' => "DVD",
            'J' => "Journal",
            _ => "Unknown"
        };
    }
    
    static string GetUserTypeName(char userType)
    {
        return userType switch
        {
            'S' => "Student",
            'R' => "Regular",
            _ => "Unknown"
        };
    }
}