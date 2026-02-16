using System;

public class Solution
{
    public bool IsAttendanceValid(
        int loginMinute,
        int totalMinutesPresent,
        bool biometricVerified
    )
    {
        if(biometricVerified == true && totalMinutesPresent >= 45 && loginMinute <= 10){
            return true;
        }
        return false;
    }

    static void Main(){
        string input = Console.ReadLine();
        string[] parts = input.Split(",");

        int loginMinute = Convert.ToInt32(parts[0]);
        int totalMinutesPresent = Convert.ToInt32(parts[1]);
        bool biometricVerified = Convert.ToBoolean(parts[2]);

        Solution obj = new Solution();
        bool result = obj.IsAttendanceValid(loginMinute, totalMinutesPresent, biometricVerified);
        Console.WriteLine(result);
    }
}
