using System;

abstract class Employee
{
    public abstract decimal CalculatePay();
}

class HourlyEmployee : Employee
{
    private decimal rate;
    private decimal hours;

    public HourlyEmployee(decimal rate, decimal hours)
    {
        this.rate = rate;
        this.hours = hours;
    }

    public override decimal CalculatePay()
    {
        return rate * hours;
    }
}

class SalariedEmployee : Employee
{
    private decimal monthlySalary;

    public SalariedEmployee(decimal monthlySalary)
    {
        this.monthlySalary = monthlySalary;
    }

    public override decimal CalculatePay()
    {
        return monthlySalary;
    }
}

class CommissionEmployee : Employee
{
    private decimal commission;
    private decimal baseSalary;

    public CommissionEmployee(decimal commission, decimal baseSalary)
    {
        this.commission = commission;
        this.baseSalary = baseSalary;
    }

    public override decimal CalculatePay()
    {
        return commission + baseSalary;
    }
}

class Program
{
    static void Main()
    {
        string[] employees =
        {
            "H 100 8",
            "S 30000",
            "C 5000 20000"
        };

        decimal totalPay = ComputeTotalPayroll(employees);
        Console.WriteLine(totalPay.ToString("F2"));
    }

    static decimal ComputeTotalPayroll(string[] employees)
    {
        decimal total = 0;

        foreach(string emp in employees)
        {
            string[] parts = emp.Split(' ');
            Employee employee = null;

            if(parts[0] == "H")
            {
                decimal rate = decimal.Parse(parts[1]);
                decimal hours = decimal.Parse(parts[2]);
                employee = new HourlyEmployee(rate, hours);
            }

            else if(parts[0] == "S")
            {
                decimal salary = decimal.Parse(parts[1]);
                employee = new SalariedEmployee(salary);
            }

            else if(parts[0] == "C")
            {
                decimal commission = decimal.Parse(parts[1]);
                decimal baseSalary = decimal.Parse(parts[2]);
                employee = new CommissionEmployee(commission, baseSalary);
            }

            total += employee.CalculatePay();
        }

        return Math.Round(total, 2, MidpointRounding.AwayFromZero);
    }
}