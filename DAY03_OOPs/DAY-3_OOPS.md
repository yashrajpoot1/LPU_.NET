# DAY 3 - Object-Oriented Programming (OOPs)

## P1: Basics OOPs

```csharp
public class Student
{
    #region Declaration
    public int id;
    public String name;
    public double MathMarks;
    public double PhysicsMarks;
    public double ChemMarks;
    #endregion

    #region constructor

    public Student()
    {
        id = 0;
        name = "";
        MathMarks = 0.0;
        PhysicsMarks = 0.0;
        ChemMarks = 0.0;
    }
    #endregion

    #region Member Functions
    /// <summary>
    /// This method calculates total marks of student
    /// </summary>
    /// <returns></returns>
    public double TotalMarks()
    {
        return MathMarks + PhysicsMarks + ChemMarks;
    }
    #endregion

}
```

```csharp
using System;

/// <summary>
/// This is Program class
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program class
    /// </summary>
    
       public static void Main(String[] args)
    {

        // Student student = new Student()
        // {
        //     id = 1000,
        //     name = "Ali",
        //     MathMarks = 90,
        //     PhysicsMarks = 80,
        //     ChemMarks = 70
        // };
        Student std1 = new Student();
        std1.id = 1001;
        std1.name = "Asad";
        std1.MathMarks = 96;
        std1.PhysicsMarks = 85;
        std1.ChemMarks = 75;
        Console.WriteLine("This is the total marks of " + std1.name + ": " + std1.TotalMarks());
    }
}
```

## P2: Inheritance

```csharp
public class Person
{
    public int id;
    public string name { get; set; }
    public int age { get; set; }
}

public class Man : Person
{
    public string playing { get; set; }
}

public class Women : Person
{
    public string PlayManage { get; set; }
}

public class Child : Person
{
    public string WatchingCartoon { get; set; }
}
```

```csharp
using System;
using System.Security.Cryptography.X509Certificates;

class Program
{

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(String[] args)
    {
        Person person = new Person() { age = 18, id = 1, name = "Asad" };
        Man man = new Man() { age = 20, id = 2, name = "Abhi", playing = "FootBall" };
        Women women = new Women() { age = 22, id = 3, name = "rohit", PlayManage = "Cricket" };
        Child child = new Child() { age = 12, id = 4, name = "babu", WatchingCartoon = "Doremon" };

    }


       public string GetManDetails (Man input)
    {
        return $" Id = { input.id}  Name = {input.name}";
    }

    public string GetWomanDetails(Women input)
    {
        return $" Id = {input.id}  Name = {input.name}";
    }

    // Note: This method won't work because Person doesn't have 'playing' property
    // public string GetDetails(Person person)
    // {
    //     return $" Id = {person.id}  Name = {person.name} playing = {person.playing}";
    // }
    
    public string GetChildDetails(Child input)
    {
        return $" Id = {input.id}  Name = {input.name}";
    }
}
```

## PQ: Inheritance (Polymorphism)

```csharp
/// <summary>
/// Summary description for Person class
/// </summary>
public class Person
{
    public int id;
    public string name { get; set; }
    public int age { get; set; }
}

/// <summary>
/// Summary description for Man class
/// </summary>

public class Man : Person
{
    public string playing { get; set; }
}

/// <summary>
///     Summary description for Women class
/// </summary>
public class Women : Person
{
    public string PlayManage { get; set; }
}


/// <summary>
///     Summary description for Child class 
/// </summary>
public class Child : Person
{
    public string WatchingCartoon { get; set; }
}
```

```csharp
using System;
using System.Security.Cryptography.X509Certificates;

class Program
{

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(String[] args)
    {
        Program program = new Program();

        Person person = new Person() { age = 18, id = 1, name = "Asad" };
        Man man = new Man() { age = 20, id = 2, name = "Abhi", playing = "FootBall" };
        Women women = new Women() { age = 22, id = 3, name = "avinanadan", PlayManage = "Cricket" };
        Child child = new Child() { age = 12, id = 4, name = "babu", WatchingCartoon = "Doremon" };

        string output = program.GetDetails(child);

        Console.WriteLine(output);
    }

    public string GetDetails(Person person)
    {
        string result = string.Empty;

        if (person is Person)
        {
            result = $" Id = {person.id}  Name = {person.name} age = {person.age} ";
        }
        if (person is Man)
        {
            Man man = (Man)person;
            result = $" Id = {man.id}  Name = {man.name} age = {man.age} playing = {man.playing}";
        }
        if (person is Women)
        {
            Women women = (Women)person;
            result = $" Id = {women.id}  Name = {women.name} age = {women.age} PlayManage = {women.PlayManage}";
        }
        if (person is Child)
        {
            Child child = (Child)person;
            result = $" Id = {child.id}  Name = {child.name} age = {child.age} WatchingCartoon = {child.WatchingCartoon}";
        }
        
        return result;
    }
}
```

## PQ3: Constructor

```csharp
/// <summary>
/// Summary description for Person class
/// </summary>
public class Person
{
    private Person()
    {
        id = 0;
        name = string.Empty;
        age = 0;
    }
    public Person(int id, string name, int age)
    {
        this.id = id;
        this.name = name;
        this.age = age;
    }
    public int id;
    public string name { get; set; }
    public int age { get; set; }
}

/// <summary>
/// Summary description for Man class
/// </summary>

public class Man : Person
{
    private Man()
    {
        playing = string.Empty;
    }
    public Man(int id, string name, int age, string playing) : base(id, name, age)
    {
        this.playing = playing;
    }

    public string playing { get; set; }
}
```

```csharp
/// <summary>
///     Summary description for Women class
/// </summary>
public class Women : Person
{
    private Women()
    {
        PlayManage = string.Empty;
    }
    public Women(int id, string name, int age, string playManage) : base(id, name, age)
    {
        this.PlayManage = playManage;
    }
    public string PlayManage { get; set; }
}
```

```csharp
/// <summary>
///     Summary description for Child class 
/// </summary>
public class Child : Person
{
    private Child()
    {
        WatchingCartoon = string.Empty;
    }
    public Child(int id, string name, int age, string watchingCartoon) : base(id, name, age)
    {
        this.WatchingCartoon = watchingCartoon;
    }
    public string WatchingCartoon { get; set; }
}
```

```csharp
using System;
using System.Security.Cryptography.X509Certificates;

class Program
{

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(String[] args)
    {
        Program program = new Program();


        // Person person = new Person() { age = 18, id = 1, name = "Asad" };
        // Man man = new Man() { age = 20, id = 2, name = "Abhi", playing = "FootBall" };
        // Women women = new Women() { age = 22, id = 3, name = "avinanadan", PlayManage = "Cricket" };
        // Child child = new Child() { age = 12, id = 4, name = "babu", WatchingCartoon = "Doremon" };
        
        
        Person person = new Person(18, "Asad", 1); //because constructor is private

        string output = program.GetDetails(person);

        Console.WriteLine(output);
    }

    public string GetDetails(Person person)
    {
        string result = string.Empty;

        if (person is Person)
        {
            result = $" Id = {person.id}  Name = {person.name} age = {person.age} ";
        }
        if (person is Man)
        {
            Man man = (Man)person;
            result = $" Id = {man.id}  Name = {man.name} age = {man.age} playing = {man.playing}";
        }
        if (person is Women)
        {
            Women women = (Women)person;
            result = $" Id = {women.id}  Name = {women.name} age = {women.age} PlayManage = {women.PlayManage}";
        }
        if (person is Child)
        {
            Child child = (Child)person;
            result = $" Id = {child.id}  Name = {child.name} age = {child.age} WatchingCartoon = {child.WatchingCartoon}";
        }
        
        return result;
    }
}
```
