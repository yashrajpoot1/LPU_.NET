# Day 4 - Object-Oriented Programming (OOP) Notes

## Key Concepts

- A class is a collection of member variables, member functions, and events.
- When we call a private constructor, it will throw an error, and with a parameterized constructor we can avoid creating empty objects to save memory.
- In parameters, we can pass one or more properties from a class.
- `this.` helps to tell that this variable is from the class level.
- Constructor does not have a return type and has the same name as the class name.
- Only in a constructor can get-only properties be set.
- **Field**: It is a private variable and can be accessed within the class only.
- Using a function in the same class, we can get the private variable outside.

---

## Sample Code Examples

### P1: Constructor Example

```csharp
namespace ConstructorEx
{
    public class Visitor
    {
        public int Id{ get; set; }
        public String? Name{ get; set; }
        public String? Requirement{ get; set; }

        // public Visitor()
        // {

        // }

        // private Visitor()
        // {

        // }

        public Visitor(int id, String name, String requirement)
        {
            this.Id = id;
            this.Name = name;
            if (requirement.Contains("Money"))
            {
                throw new ArgumentException("You Bhikhari");
            }
            this.Requirement = requirement;
        }

        // public Visitor(int id, String name)
        // {
        //     Id = id;
        //     if (id == 0)
        //         Console.WriteLine("Id should not be zero");

        //     if (name.Contains("Faltu"))
        //         throw new ArgumentException("Don't use word like faltu");
        //     Name = name;
        // }

        // public Visitor(int id)
        // {
        //     Id = id;
        // }
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
// using ConstructorEx;

/// <summary>
/// Learning Constructor 
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            var visitor = new Visitor(12, "Asad", "Money for Books");
            Console.WriteLine(visitor.Name + " " + visitor.Id);

            Visitor visitor1 = new Visitor(12);
            Console.WriteLine("Id is " + visitor1.Id);

            Visitor visitor2 = new Visitor(0, "Asad");
            Console.WriteLine("Name and ID" + visitor2.Id + " " + visitor2.Name);


        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P2: Get-only Properties Example

```csharp
public class Add
{
    public int Num1 { get; set; }
    public int Num2 { get; set; }
    public int Result { get; }

    public Add(int A, int B)
    {
        Num1 = A;
        Num2 = B;
        Result = A + B; // Only in constructor get properties can be set
    }

}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
// using ConstructorEx;

/// <summary>
/// Learning Constructor 
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            String? input1, input2;
            Console.WriteLine("Enter First Number");
            input1 = Console.ReadLine();
            Console.WriteLine("Enter Second Number");
            input2 = Console.ReadLine();
            int num1 = Convert.ToInt32(input1);
            int num2 = Convert.ToInt32(input2);

            Add add = new Add(num1, num2);
            Console.WriteLine("Result is " + add.Result);

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P3: Constructor Chaining

```csharp
namespace ConstructorEx
{

    /// <summary>
    /// This is Visitor class and here we are learning Constructor chaining
    /// </summary>
    public class Visitor
    {
        public int Id{ get; set; }
        public string? Name{ get; set; }
        public string? Requirement { get; set; }
        
        public string? LogHistory { get; set; }

        public Visitor() // Default constructor
        {
            LogHistory = "Default constructor called at " + DateTime.Now + "\n";
        }
        public Visitor(int id) : this() // this() is calling default constructor
        {
            LogHistory += "Id constructor called at " + DateTime.Now + "\n";
            Id = id;
        }

        public Visitor(int id, string name) : this(id) // this(id) is calling constructor with one parameter
        {
            // Id = id;
            if (id == 0)
                Console.WriteLine("Id should not be zero");

            if (name.Contains("Faltu"))
                throw new ArgumentException("Don't use word like faltu");
                
            LogHistory += $"name constructor called at " + DateTime.Now + "\n";
            Name = name;
        }

        public Visitor(int id, string name, string requirement) : this(id,name) // this(id,name) is calling constructor with two parameters
        {
            // this.Id = id;
            // this.Name = name;
            if (requirement.Contains("Money"))
            {
                throw new ArgumentException("You Bhikhari");
            }
            
            LogHistory += "Requirement constructor called at " + DateTime.Now + "\n";
            Requirement = requirement;
        }
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
using ConstructorEx;

/// <summary>
/// Learning Constructor 
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            Visitor visitor = new Visitor(12, "Asad", "Books"); // Creating object of Visitor class by passing three parameters
            Console.WriteLine(visitor.LogHistory); // Printing LogHistory to see the order of constructor calls

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P4: Learning Fields

```csharp
namespace Oopssessions
{
    public class Employee
    {
        private int id;
        // public int Id{ get => id; set => id = value; }
        // so here we can validate the user input at setter like below

        public int Id
        {
            set
            {
                if (value > 0)
                {
                    id = value;
                }
                else
                {
                    id = 0;
                    throw new ArgumentException("How id can be less than zero");
                }
            }
        }
        
        public string DisplayEmpDetails()
        {
            return $"Employee Id is : {id}";
        }
        
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
using ConstructorEx;
using Oopssessions;

/// <summary>
/// Learning Field 
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            Employee employee = new Employee();
            employee.Id = 100; // setting value using setter and -1 will throw error

            //employee.id; // accessing private field directly will throw error
            Console.WriteLine(employee.DisplayEmpDetails()); // getting value using method

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P5: Field Validation Example

```csharp
namespace Oopssessions
{
    public class Associate
    {
        private int associateId;
        private string associateName;
        private int rank;
        public string ErrorM;
        public int AssociateId
        {
            get { return associateId; }
            set
            {
                if (value > 0)
                {
                    associateId = value;
                }
                else
                {
                    associateId = 0;
                    ErrorM += "Associate Id must be greater than zero And \n";
                }
            }
        }

        public string AssociateName
        {
            get { return associateName; }
            set
            {
                if (value.Contains("  "))
                {
                    associateName = "";
                    ErrorM += "Associate name should not contains 2 spaces And \n";
                }
                else
                {
                    associateName = value;
                }
            }
        }
        
        public int AssociateRank
        {
            get { return rank; }
            set
            {
                if (value > 0)
                {
                    rank = value;
                }
                else
                {
                    rank = 0;
                    ErrorM += "Associate Rank must be zero or positive \n";
                }
            }
        }
        

        public string GetAssociateDetails()
        {
            return $"Associate Id : {associateId} \nAssociate Name : {associateName} \nAssociate Rank : {rank}";
        }
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
using ConstructorEx;
using Oopssessions;

/// <summary>
/// This is Program class
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            Associate associate = new Associate();
            associate.AssociateId = -101;
            associate.AssociateName = "John  Doe";
            associate.AssociateRank = -1;
            Console.WriteLine("Errors: "+ associate.ErrorM);
            Console.WriteLine(associate.GetAssociateDetails());

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P6: Inheritance and Encapsulation

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace OopsSessions
{
    /// <summary>
    ///     This is Account base class 
    /// </summary>
    public class Account
    {
        public string Name { get; set; }
        public int AccountId { get; set; }

        public string GetAccountDetails()
        {
            return $"I am Base account and My Id is {AccountId}";
        }

    }
    /// <summary>
    ///   This is SalesAccount derived class
    /// </summary>
    public class SalesAccount : Account
    {
        public string GetSalesAccountDetails()
        {
            string info = string.Empty;
            info += base.GetAccountDetails();
            info += " I am from Sales Derived class ";
            return info;
        }
        public string SalesInfo { get; set; }
    }
    /// <summary>
    ///   This is PurchaseAccount derived class
    /// </summary>
    // public class PurchaseAccount : Account
    // {
    //     public string GetPurchaseAccountDetails()
    //     {
    //         string pinfo = string.Empty;
    //         pinfo += base.GetAccountDetails();
    //         pinfo += " I am from purchase derived class ";
    //         return pinfo;
    //     }
    //     public string PurchaseInfo { get; set; }
    // }


    public class PurchaseAccount : SalesAccount
    {
        public string GetPurchaseAccountDetails()
        {
            string pinfo = string.Empty;
            pinfo += base.GetSalesAccountDetails();
            pinfo += " I am from purchase derived class ";
            return pinfo;
        }
        public string PurchaseInfo { get; set; }
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
using OopsSessions;

/// <summary>
/// This is Program class
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            Account account = new Account() { AccountId = 1, Name = "Account1" };
            string result = account.GetAccountDetails(); // Calling base class method
            Console.WriteLine(result);

            SalesAccount salesAccount = new SalesAccount() { AccountId = 1, Name = "Balu", SalesInfo = "" };
            var result1 = salesAccount.GetSalesAccountDetails(); // Calling derived class method
            Console.WriteLine(result1);

            PurchaseAccount purchaseAccount = new PurchaseAccount() { AccountId = 1, Name = "Ali", PurchaseInfo = "purchased" };
            var result2 = purchaseAccount.GetPurchaseAccountDetails(); // Calling derived class method
            Console.WriteLine(result2);

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
```

---

### P7: Method Override, Virtual Method, Base and Derived Class

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace OopsSessions
{
    /// <summary>
    /// this is Account base class i am learning inheritance concept 
    /// </summary>
    public class Account
    {
        public virtual string GetAccountDetails()
        {
            return "I am from Account base class ";
        }

    }
    /// <summary>
    /// this is SavingsAccount derived class and inheriting Account base class and it is derived class and overriding GetAccountDetails method
    /// </summary>
    public class SavingsAccount : Account
    {
        public override string GetAccountDetails()
        {
            return "I am from SavingsAccount derived class ";
        }
    }
}
```

---

### P8: Virtual and Override Example

```csharp
namespace Oopssessions
{
    /// <summary>
    /// This is Employee base class 
    /// </summary>
    public class Employee
    {
        public virtual string GetFestivals()
        {
            return "This is the list of Holidays";
        }

    }
    
    /// <summary>
    /// This is Indian derived class inheriting Employee base class
    /// </summary>
    public class Indian : Employee
    {
        public override string GetFestivals()
        {
            String Holidays = "Diwali, Holi, Eid";
            return $"List of Indian Festivals {Holidays}";
        }
    }

/// <summary>
/// this is USA derived class inheriting Employee base class
/// </summary>

    public class USA : Employee
    {
        public override string GetFestivals()
        {
            String Holidays = "Thanksgiving, Christmas, Independence Day";
            return $"List of South Indian Festivals {Holidays}";
        }
    }
}
```

```csharp
using System;
using System.Reflection.PortableExecutable;
using Oopssessions;

/// <summary>
/// This is Program class
/// </summary>

class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            Indian indian = new Indian();
            Console.WriteLine(indian.GetFestivals());
            USA usa = new USA();
            Console.WriteLine(usa.GetFestivals());

        }
        catch(Exception err)
        {
            Console.WriteLine("Error " + err.Message);
        }
    }
}
