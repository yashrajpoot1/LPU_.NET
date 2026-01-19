## 🧑‍💼 Person Details

### Problem Statement

Your task here is to implement a **C# code** based on the following specifications.
Note that your code should match the specifications in a **precise manner**.
Consider **default visibility** of classes, data fields, and methods unless mentioned otherwise.

---

### Specifications

#### Class Definitions

##### Class `Person`

**Method / Property Definitions:**

- `Name`

  - Implement getter and setter method
  - Use **Auto-Implemented Property**
  - Visibility: `public`
  - Return Type: `string`

- `Address`

  - Implement getter and setter method
  - Use **Auto-Implemented Property**
  - Visibility: `public`
  - Return Type: `string`

- `Age`

  - Implement getter and setter method
  - Use **Auto-Implemented Property**
  - Visibility: `public`
  - Return Type: `int`

---

##### Class `PersonImplementation`

**Method Definitions:**

- `GetName(IList<Person> persons)`

  - Display the **name and address** of all the persons in the list
  - Visibility: `public`
  - Return Type: `string`

- `Average(IList<Person> persons)`

  - Method to calculate the **average age** of all persons
  - Visibility: `public`
  - Return Type: `double`

- `Max(IList<Person> persons)`

  - Method to calculate the **maximum age** among all persons
  - Visibility: `public`
  - Return Type: `int`

---

### Task

- Create a class **`Person`** with attributes `string Name`, `string Address`, and `int Age`, and define getter and setter methods using **Auto-Implemented Properties**.
- Create a class **`PersonImplementation`** and implement the following methods:

  1. `GetName(IList<Person> persons)` – display the name and address of all the persons in the list.
  2. `Average(IList<Person> persons)` – calculate the average age of all the persons.
  3. `Max(IList<Person> persons)` – find the maximum age among the persons.

---

### Sample Input

```csharp
IList<Person> p = new List<Person>();

p.Add(new Person { Name = "Aarya", Address = "A2101", Age = 69 });
p.Add(new Person { Name = "Daniel", Address = "D104", Age = 40 });
p.Add(new Person { Name = "Ira", Address = "H801", Age = 25 });
p.Add(new Person { Name = "Jennifer", Address = "I1704", Age = 33 });
```

---

### Sample Output

```
Aarya A2101 Daniel D104 Ira H801 Jennifer I1704
41.75
69
```

---

### Important Notes

- If you want to test your program, you can implement a `Main()` method in the stub.
- Use the **RUN CODE** option to test your program.
- Ensure valid function calls with valid data.

---


```csharp
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Practice
{
    // Person class with Name, Address, and Age properties
    public class Person
    {
        public string Name { get; set; } // Person's name
        public string Address { get; set; } // Person's address
        public int Age { get; set; } // Person's age
    }

    // PersonImplementation class with methods to process a list of Person objects
    public class PersonImplementation
    {
        // Method to concatenate names and addresses of all persons in the list
        /// <summary>
        /// Concatenates names and addresses of all persons in the list.
        /// </summary>
        public string GetName(IList<Person> person)
        {
            string result = "";
            foreach (var p in person)
            {
                result += p.Name + " " + p.Address;
            }
            return result;
        }

        // Method to calculate the average age of all persons in the list
        /// <summary>
        /// Calculates the average age of all persons in the list.
        /// </summary>
        public double Average(IList<Person> person)
        {
            double totalAge = 0;
            foreach (var p in person)
            {
                totalAge += p.Age;
            }
            return totalAge / person.Count;
        }

        // Method to find the maximum age among all persons in the list
        /// <summary>
        /// Finds the maximum age among all persons in the list.
        /// </summary>
        public int MaxAge(IList<Person> person)
        {
            int maxAge = 0;
            foreach (var p in person)
            {
                if (p.Age > maxAge)
                {
                    maxAge = p.Age;
                }
            }
            return maxAge;
        }
    }
}

```
### Program.cs

```csharp
namespace Practice
{
    // Main program to demonstrate Person and PersonImplementation functionality
    public class Program
    {
        // Entry point of the program
        static void Main(string[] args)
        {
            IList<Person> persons = new List<Person>(); // Creating a list to hold Person objects
            persons.Add(new Person { Name = "Alice", Address = "123 Main St", Age = 30 }); // Adding sample Person objects to the list
            persons.Add(new Person { Name = "Bob", Address = "456 Oak Ave", Age = 25 });
            persons.Add(new Person { Name = "Charlie", Address = "789 Pine Rd", Age = 35 });
            persons.Add(new Person { Name = "Diana", Address = "321 Maple Ln", Age = 28 });

            PersonImplementation personImplementation = new PersonImplementation(); // Creating an instance of PersonImplementation to use its methods
            Console.WriteLine(personImplementation.GetName(persons)); // Calling GetName method and printing the result
            Console.WriteLine(personImplementation.Average(persons)); // Calling Average method and printing the result
            Console.WriteLine(personImplementation.MaxAge(persons)); // Calling MaxAge method and printing the result
        }
    }
}

```