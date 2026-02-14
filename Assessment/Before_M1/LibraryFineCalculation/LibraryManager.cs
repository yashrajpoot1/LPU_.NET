namespace LibraryFineCalculation;

public class LibraryManager
{
    private readonly SortedDictionary<decimal, LibraryUser> _members = new();

    public void AddMember(LibraryUser member)
    {
        _members[member.OutstandingFine] = member;
        Console.WriteLine($"✓ Added: {member.Name} (Fine: ${member.OutstandingFine:F2})");
    }

    public void PayFine(string memberId, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidPaymentException(amount);

        var member = _members.Values.FirstOrDefault(m => m.MemberId == memberId);
        if (member == null)
            throw new FineNotFoundException(memberId);

        var oldFine = member.OutstandingFine;
        _members.Remove(oldFine);

        member.DaysOverdue = Math.Max(0, (int)((oldFine - amount) / (member is StudentMember ? 0.50m : 0.25m)));
        member.CalculateFine();

        if (member.OutstandingFine > 0)
            _members[member.OutstandingFine] = member;

        Console.WriteLine($"✓ Payment received: ${amount:F2}. Remaining fine: ${member.OutstandingFine:F2}");
    }

    public void DisplayMembers()
    {
        Console.WriteLine("\n========== MEMBERS (Sorted by Fine) ==========");
        foreach (var kvp in _members)
        {
            kvp.Value.DisplayInfo();
        }
    }

    public int GetTotalMembers() => _members.Count;
}

public class LibraryException : Exception
{
    public LibraryException(string message) : base(message) { }
}

public class FineNotFoundException : LibraryException
{
    public FineNotFoundException(string memberId)
        : base($"Member {memberId} not found") { }
}

public class InvalidPaymentException : LibraryException
{
    public InvalidPaymentException(decimal amount)
        : base($"Invalid payment amount: ${amount:F2}. Must be positive") { }
}
