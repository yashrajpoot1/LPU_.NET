namespace LibraryFineCalculation;

public abstract class LibraryUser
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int DaysOverdue { get; set; }
    public decimal OutstandingFine { get; protected set; }

    protected LibraryUser(string memberId, string name, int age, int daysOverdue)
    {
        MemberId = memberId;
        Name = name;
        Age = age;
        DaysOverdue = daysOverdue;
        CalculateFine();
    }

    public abstract void CalculateFine();
    public abstract string GetMemberType();

    public void DisplayInfo()
    {
        Console.WriteLine($"[{GetMemberType()}] {Name} (ID: {MemberId})");
        Console.WriteLine($"  Days Overdue: {DaysOverdue}, Fine: ${OutstandingFine:F2}");
    }
}

public class StudentMember : LibraryUser
{
    public StudentMember(string memberId, string name, int age, int daysOverdue)
        : base(memberId, name, age, daysOverdue) { }

    public override void CalculateFine()
    {
        OutstandingFine = DaysOverdue * 0.50m; // $0.50 per day
    }

    public override string GetMemberType() => "Student";
}

public class FacultyMember : LibraryUser
{
    public FacultyMember(string memberId, string name, int age, int daysOverdue)
        : base(memberId, name, age, daysOverdue) { }

    public override void CalculateFine()
    {
        OutstandingFine = DaysOverdue * 0.25m; // $0.25 per day (discounted)
    }

    public override string GetMemberType() => "Faculty";
}
