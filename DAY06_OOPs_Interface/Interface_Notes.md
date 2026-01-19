## 1️⃣ What is an Interface in C#? (In simple words)

An **interface** is a **contract**.

👉 It tells a class **WHAT to do**, not **HOW to do it**.

If a class **implements** an interface, it **must implement all its methods**.

---

### Real-life analogy

Think of **USB standard**:

* USB defines **rules**
* Any device (mouse, keyboard, pen drive) **must follow those rules**
* Each device works differently internally

➡️ USB = Interface
➡️ Mouse/Keyboard = Implementing classes

---

## 2️⃣ Key Rules of Interface (Very Important)

| Rule                         | Explanation                             |
| ---------------------------- | --------------------------------------- |
| No method body               | Only method declaration                 |
| No fields                    | Only methods, properties                |
| Multiple inheritance allowed | One class can implement many interfaces |
| Default access               | All members are **public**              |
| Cannot create object         | `new IInterface()` ❌                    |

---

## 3️⃣ Syntax of Interface

```csharp
public interface IMyInterface
{
    void DoWork();
}
```

Implementation:

```csharp
public class MyClass : IMyInterface
{
    public void DoWork()
    {
        Console.WriteLine("Working...");
    }
}
```

---

# 🟢 EASY Example (Beginner Level)

### Scenario: Payment System

We don’t care **how** payment happens—only that payment happens.

---

### Interface

```csharp
public interface IPayment
{
    void Pay(int amount);
}
```

---

### Implementation 1: Cash Payment

```csharp
public class CashPayment : IPayment
{
    public void Pay(int amount)
    {
        Console.WriteLine("Paid " + amount + " using Cash");
    }
}
```

---

### Implementation 2: Card Payment

```csharp
public class CardPayment : IPayment
{
    public void Pay(int amount)
    {
        Console.WriteLine("Paid " + amount + " using Card");
    }
}
```

---

### Usage

```csharp
IPayment payment = new CashPayment();
payment.Pay(500);
```

✅ **Why Interface here?**

* Tomorrow you add `UPIPayment`
* No change in existing code (Open–Closed Principle)

---

# 🟡 MEDIUM Example (Real OOP + Interview Level)

### Scenario: Employee Bonus Calculation

Different employees → different bonus logic.

---

### Interface

```csharp
public interface IBonusCalculator
{
    double CalculateBonus(double salary);
}
```

---

### Permanent Employee

```csharp
public class PermanentEmployee : IBonusCalculator
{
    public double CalculateBonus(double salary)
    {
        return salary * 0.20;
    }
}
```

---

### Contract Employee

```csharp
public class ContractEmployee : IBonusCalculator
{
    public double CalculateBonus(double salary)
    {
        return salary * 0.10;
    }
}
```

---

### HR Service (Using Interface)

```csharp
public class HRService
{
    public void PrintBonus(IBonusCalculator employee, double salary)
    {
        Console.WriteLine("Bonus: " + employee.CalculateBonus(salary));
    }
}
```

---

### Main

```csharp
HRService hr = new HRService();

IBonusCalculator emp1 = new PermanentEmployee();
hr.PrintBonus(emp1, 50000);

IBonusCalculator emp2 = new ContractEmployee();
hr.PrintBonus(emp2, 40000);
```

✅ **Senior-level point**

* HRService is **loosely coupled**
* No `if-else` or `switch`
* Easy to extend

---

# 🔴 HARD Example (Industry-Level Architecture)

### Scenario: Notification System (Email, SMS, WhatsApp)

Used in:

* Banking apps
* HR portals
* E-commerce

---

## Step 1: Interface

```csharp
public interface INotificationService
{
    void Send(string message);
}
```

---

## Step 2: Implementations

### Email

```csharp
public class EmailNotification : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}
```

### SMS

```csharp
public class SmsNotification : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}
```

### WhatsApp

```csharp
public class WhatsAppNotification : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine("WhatsApp message sent: " + message);
    }
}
```

---

## Step 3: Business Logic Layer (Very Important)

```csharp
public class NotificationManager
{
    private readonly INotificationService _notification;

    public NotificationManager(INotificationService notification)
    {
        _notification = notification;
    }

    public void Notify(string message)
    {
        _notification.Send(message);
    }
}
```

---

## Step 4: Main (Dependency Injection concept)

```csharp
INotificationService service = new EmailNotification();
NotificationManager manager = new NotificationManager(service);
manager.Notify("Your salary is credited");
```

---

## 🔥 Why this is HARD & IMPRESSIVE

✔ Loose coupling
✔ Dependency Injection
✔ Follows SOLID principles
✔ Easily testable
✔ Used in real enterprise apps

---

## Interface vs Abstract (Quick Interview Answer)

| Interface            | Abstract Class       |
| -------------------- | -------------------- |
| 100% abstraction     | Partial abstraction  |
| Multiple inheritance | Single inheritance   |
| No fields            | Can have fields      |
| No constructor       | Can have constructor |

---

## When to Use Interface? (Golden Rule)

Use **interface** when:

* You need **multiple implementations**
* You want **loose coupling**
* You want to follow **SOLID principles**
* You are designing **APIs or frameworks**

---
