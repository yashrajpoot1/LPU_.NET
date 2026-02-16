using NUnit.Framework;

[TestFixture]
public class UnitTest
{
    [Test]
    public void Test_Deposit_ValidAmount()
    {
        Program account = new Program(100m);
        account.Deposit(50m);

        Assert.AreEqual(150m, account.Balance);
    }

    [Test]
    public void Test_Deposit_NegativeAmount()
    {
        Program account = new Program(100m);

        Assert.AreEqual(
            "Deposit amount cannot be negative",
            Assert.Throws<System.Exception>(() => account.Deposit(-10m)).Message
        );
    }

    [Test]
    public void Test_Withdraw_ValidAmount()
    {
        Program account = new Program(200m);
        account.Withdraw(80m);

        Assert.AreEqual(120m, account.Balance);
    }

    [Test]
    public void Test_Withdraw_InsufficientFunds()
    {
        Program account = new Program(50m);

        Assert.AreEqual(
            "Insufficient funds.",
            Assert.Throws<System.Exception>(() => account.Withdraw(100m)).Message
        );
    }
}
