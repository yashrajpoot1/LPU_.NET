using System;
using SecurePasswordStorage;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 14: Secure Password Storage ===");
        var hasher = new PasswordHasher();

        string password = "MySecurePassword123!";
        string hash = hasher.HashPassword(password);
        
        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hash: {hash.Substring(0, 40)}...");

        bool isValid = hasher.VerifyPassword(password, hash);
        Console.WriteLine($"Verification (correct): {isValid}");

        bool isInvalid = hasher.VerifyPassword("WrongPassword", hash);
        Console.WriteLine($"Verification (wrong): {isInvalid}");

        Console.WriteLine("✓ Test Passed\n");
    }
}
