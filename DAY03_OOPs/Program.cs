using System;

namespace DAY03_OOPs
{
    /// <summary>
    /// Day 3 - Object-Oriented Programming (OOPs)
    /// Contains examples of classes, inheritance, and polymorphism
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point for the application
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("=== DAY 3: Object-Oriented Programming ===\n");
            
            bool continueProgram = true;
            
            while (continueProgram)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        StudentExample();
                        break;
                    case "2":
                        InheritanceExample();
                        break;
                    case "3":
                        PolymorphismExample();
                        break;
                    case "4":
                        ConstructorExample();
                        break;
                    case "0":
                        continueProgram = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                if (continueProgram)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Displays the main menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Student Class Example");
            Console.WriteLine("2. Inheritance Example");
            Console.WriteLine("3. Polymorphism Example");
            Console.WriteLine("4. Constructor Example");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Demonstrates basic class usage with Student
        /// </summary>
        static void StudentExample()
        {
            Console.WriteLine("\n=== Student Class Example ===");
            
            Student std1 = new Student();
            std1.id = 1001;
            std1.name = "Asad";
            std1.MathMarks = 96;
            std1.PhysicsMarks = 85;
            std1.ChemMarks = 75;
            
            Console.WriteLine($"Student ID: {std1.id}");
            Console.WriteLine($"Student Name: {std1.name}");
            Console.WriteLine($"Math Marks: {std1.MathMarks}");
            Console.WriteLine($"Physics Marks: {std1.PhysicsMarks}");
            Console.WriteLine($"Chemistry Marks: {std1.ChemMarks}");
            Console.WriteLine($"Total Marks: {std1.TotalMarks()}");
            Console.WriteLine($"Average: {std1.TotalMarks() / 3:F2}");
        }

        /// <summary>
        /// Demonstrates inheritance with Person hierarchy
        /// </summary>
        static void InheritanceExample()
        {
            Console.WriteLine("\n=== Inheritance Example ===");
            
            Person person = new Person() { age = 18, id = 1, name = "Asad" };
            Man man = new Man() { age = 20, id = 2, name = "Abhi", playing = "FootBall" };
            Women women = new Women() { age = 22, id = 3, name = "Rohit", PlayManage = "Cricket" };
            Child child = new Child() { age = 12, id = 4, name = "Babu", WatchingCartoon = "Doremon" };

            Console.WriteLine($"Person: ID={person.id}, Name={person.name}, Age={person.age}");
            Console.WriteLine($"Man: ID={man.id}, Name={man.name}, Age={man.age}, Playing={man.playing}");
            Console.WriteLine($"Women: ID={women.id}, Name={women.name}, Age={women.age}, PlayManage={women.PlayManage}");
            Console.WriteLine($"Child: ID={child.id}, Name={child.name}, Age={child.age}, WatchingCartoon={child.WatchingCartoon}");
        }

        /// <summary>
        /// Demonstrates polymorphism
        /// </summary>
        static void PolymorphismExample()
        {
            Console.WriteLine("\n=== Polymorphism Example ===");
            
            Program program = new Program();
            
            Person person = new Person() { age = 18, id = 1, name = "Asad" };
            Man man = new Man() { age = 20, id = 2, name = "Abhi", playing = "FootBall" };
            Women women = new Women() { age = 22, id = 3, name = "Avinandan", PlayManage = "Cricket" };
            Child child = new Child() { age = 12, id = 4, name = "Babu", WatchingCartoon = "Doremon" };

            Console.WriteLine("Using polymorphic method GetDetails:");
            Console.WriteLine(program.GetDetails(person));
            Console.WriteLine(program.GetDetails(man));
            Console.WriteLine(program.GetDetails(women));
            Console.WriteLine(program.GetDetails(child));
        }

        /// <summary>
        /// Demonstrates constructor usage
        /// </summary>
        static void ConstructorExample()
        {
            Console.WriteLine("\n=== Constructor Example ===");
            
            Program program = new Program();
            
            PersonWithConstructor person = new PersonWithConstructor(1, "Asad", 18);
            ManWithConstructor man = new ManWithConstructor(2, "Abhi", 20, "FootBall");
            WomenWithConstructor women = new WomenWithConstructor(3, "Avinandan", 22, "Cricket");
            ChildWithConstructor child = new ChildWithConstructor(4, "Babu", 12, "Doremon");

            Console.WriteLine("Using constructors:");
            Console.WriteLine(program.GetDetailsWithConstructor(person));
            Console.WriteLine(program.GetDetailsWithConstructor(man));
            Console.WriteLine(program.GetDetailsWithConstructor(women));
            Console.WriteLine(program.GetDetailsWithConstructor(child));
        }

        /// <summary>
        /// Polymorphic method to get details of any Person type
        /// </summary>
        /// <param name="person">Person object or its derived type</param>
        /// <returns>Formatted string with person details</returns>
        public string GetDetails(Person person)
        {
            string result = string.Empty;

            if (person is Child child)
            {
                result = $"Child - ID: {child.id}, Name: {child.name}, Age: {child.age}, WatchingCartoon: {child.WatchingCartoon}";
            }
            else if (person is Man man)
            {
                result = $"Man - ID: {man.id}, Name: {man.name}, Age: {man.age}, Playing: {man.playing}";
            }
            else if (person is Women women)
            {
                result = $"Women - ID: {women.id}, Name: {women.name}, Age: {women.age}, PlayManage: {women.PlayManage}";
            }
            else if (person is Person)
            {
                result = $"Person - ID: {person.id}, Name: {person.name}, Age: {person.age}";
            }
            
            return result;
        }

        /// <summary>
        /// Polymorphic method for constructor-based classes
        /// </summary>
        /// <param name="person">PersonWithConstructor object or its derived type</param>
        /// <returns>Formatted string with person details</returns>
        public string GetDetailsWithConstructor(PersonWithConstructor person)
        {
            string result = string.Empty;

            if (person is ChildWithConstructor child)
            {
                result = $"Child - ID: {child.id}, Name: {child.name}, Age: {child.age}, WatchingCartoon: {child.WatchingCartoon}";
            }
            else if (person is ManWithConstructor man)
            {
                result = $"Man - ID: {man.id}, Name: {man.name}, Age: {man.age}, Playing: {man.playing}";
            }
            else if (person is WomenWithConstructor women)
            {
                result = $"Women - ID: {women.id}, Name: {women.name}, Age: {women.age}, PlayManage: {women.PlayManage}";
            }
            else if (person is PersonWithConstructor)
            {
                result = $"Person - ID: {person.id}, Name: {person.name}, Age: {person.age}";
            }
            
            return result;
        }
    }

    #region Basic Classes (Property Initialization)

    /// <summary>
    /// Student class demonstrating basic OOP concepts
    /// </summary>
    public class Student
    {
        #region Declaration
        public int id;
        public string name;
        public double MathMarks;
        public double PhysicsMarks;
        public double ChemMarks;
        #endregion

        #region Constructor
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
        /// Calculates total marks of student
        /// </summary>
        /// <returns>Sum of all subject marks</returns>
        public double TotalMarks()
        {
            return MathMarks + PhysicsMarks + ChemMarks;
        }
        #endregion
    }

    /// <summary>
    /// Base Person class
    /// </summary>
    public class Person
    {
        public int id;
        public string name { get; set; } = "";
        public int age { get; set; }
    }

    /// <summary>
    /// Man class inheriting from Person
    /// </summary>
    public class Man : Person
    {
        public string playing { get; set; } = "";
    }

    /// <summary>
    /// Women class inheriting from Person
    /// </summary>
    public class Women : Person
    {
        public string PlayManage { get; set; } = "";
    }

    /// <summary>
    /// Child class inheriting from Person
    /// </summary>
    public class Child : Person
    {
        public string WatchingCartoon { get; set; } = "";
    }

    #endregion

    #region Constructor-based Classes

    /// <summary>
    /// Person class with constructor
    /// </summary>
    public class PersonWithConstructor
    {
        public PersonWithConstructor(int id, string name, int age)
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
    /// Man class with constructor
    /// </summary>
    public class ManWithConstructor : PersonWithConstructor
    {
        public ManWithConstructor(int id, string name, int age, string playing) : base(id, name, age)
        {
            this.playing = playing;
        }

        public string playing { get; set; }
    }

    /// <summary>
    /// Women class with constructor
    /// </summary>
    public class WomenWithConstructor : PersonWithConstructor
    {
        public WomenWithConstructor(int id, string name, int age, string playManage) : base(id, name, age)
        {
            this.PlayManage = playManage;
        }

        public string PlayManage { get; set; }
    }

    /// <summary>
    /// Child class with constructor
    /// </summary>
    public class ChildWithConstructor : PersonWithConstructor
    {
        public ChildWithConstructor(int id, string name, int age, string watchingCartoon) : base(id, name, age)
        {
            this.WatchingCartoon = watchingCartoon;
        }

        public string WatchingCartoon { get; set; }
    }

    #endregion
}