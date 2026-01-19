# 🔹 What is JSON Serialization?

### Simple definition

> **JSON serialization = converting a C# object into JSON text**
> **JSON deserialization = converting JSON text back into a C# object**

```text
Object  ⇄  JSON
```

![Image](https://media.licdn.com/dms/image/v2/D4D12AQG2iS4eXNw6sA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1673434678401?e=2147483647\&t=hXBFAJvx5G9Upv7CD4rEAC7CJ_P2ZQ4BW6Ju4GUOrLU\&v=beta)

![Image](https://www.researchgate.net/publication/362120867/figure/fig2/AS%3A11431281078482603%401660071933318/JSON-schema-to-web-form-rendering-flowchart-JSON-JavaScript-Object-Notation.png)

---

## 1️⃣ JSON vs XML (Quick Context)

| Feature           | XML    | JSON        |
| ----------------- | ------ | ----------- |
| Readability       | Medium | High        |
| Size              | Large  | Smaller     |
| Speed             | Slower | Faster      |
| Used in APIs      | Rare   | VERY COMMON |
| Schema strictness | High   | Low         |

🧠 **Industry rule:**

> Modern APIs = JSON by default

---

## 2️⃣ JSON Serialization in .NET (Modern & Recommended)

In **.NET Core / .NET 5+**, the **standard library** is:

```csharp
System.Text.Json
```

✔ Fast
✔ Secure
✔ Built-in
✔ Actively maintained

---

## 3️⃣ Basic Class (Same Student You Know)

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Scores { get; set; }
}
```

---

## 4️⃣ JSON SERIALIZATION (Object → JSON)

### Code

```csharp
using System;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Student s = new Student
        {
            Id = 1,
            Name = "Ali",
            Scores = new List<int> { 10, 50, 20 }
        };

        string json = JsonSerializer.Serialize(s);
        Console.WriteLine(json);
    }
}
```

### Output

```json
{"Id":1,"Name":"Ali","Scores":[10,50,20]}
```

✔ Clean
✔ Human-readable
✔ Compact

---

## 5️⃣ JSON DESERIALIZATION (JSON → Object)

### JSON input

```json
{"Id":1,"Name":"Ali","Scores":[10,50,20]}
```

### Code

```csharp
Student s = JsonSerializer.Deserialize<Student>(json);

Console.WriteLine(s.Name);   // Ali
Console.WriteLine(s.Scores.Count); // 3
```

🧠 **Round-trip complete**

---

## 6️⃣ Pretty JSON (Industry Preference)

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true
};

string json = JsonSerializer.Serialize(s, options);
```

Output:

```json
{
  "Id": 1,
  "Name": "Ali",
  "Scores": [
    10,
    50,
    20
  ]
}
```

---

## 7️⃣ Different JSON Name Than C# Property

Same concept as `[XmlElement]`, but JSON version 👇

### Use `[JsonPropertyName]`

```csharp
using System.Text.Json.Serialization;

public class Student
{
    [JsonPropertyName("student_id")]
    public int Id { get; set; }

    [JsonPropertyName("full_name")]
    public string Name { get; set; }
}
```

JSON:

```json
{
  "student_id": 1,
  "full_name": "Ali"
}
```

🧠 **Exactly like XML mapping — different attribute**

---

## 8️⃣ Ignore Property in JSON

### Equivalent of `[XmlIgnore]`

```csharp
[JsonIgnore]
public string InternalCode { get; set; }
```

✔ Won’t appear in JSON
✔ Still exists in C# object

---

## 9️⃣ Case Sensitivity (IMPORTANT)

### JSON input

```json
{"id":1,"name":"Ali"}
```

### Default behavior

❌ Deserialization fails (case-sensitive)

### Fix (industry standard)

```csharp
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Student s = JsonSerializer.Deserialize<Student>(json, options);
```

---

## 🔟 JSON to/from FILE (Very Common)

### Write JSON to file

```csharp
File.WriteAllText("student.json", json);
```

### Read JSON from file

```csharp
string jsonFromFile = File.ReadAllText("student.json");
Student s = JsonSerializer.Deserialize<Student>(jsonFromFile);
```

---

## 1️⃣1️⃣ Dictionary in JSON (IMPORTANT DIFFERENCE FROM XML)

### JSON supports Dictionary naturally ✔

```csharp
public class Student
{
    public Dictionary<string, int> Scores { get; set; }
}
```

JSON:

```json
{
  "Scores": {
    "Math": 90,
    "Science": 80
  }
}
```

✔ This works
❌ This does NOT work in XML

---

## 1️⃣2️⃣ Common Mistakes Beginners Make ❌

* Forgetting `using System.Text.Json`
* Expecting private properties to serialize
* Not handling case-insensitive JSON
* Using `BinaryFormatter` mindset
* Confusing XML attributes with JSON properties

---

## 1️⃣3️⃣ Interview-Ready Comparison 🎯

| Topic        | XML           | JSON             |
| ------------ | ------------- | ---------------- |
| Serializer   | XmlSerializer | JsonSerializer   |
| Rename field | XmlElement    | JsonPropertyName |
| Ignore field | XmlIgnore     | JsonIgnore       |
| Dictionary   | ❌             | ✅                |
| Namespaces   | Yes           | No               |

---

## 🧠 Senior Architect Summary

> JSON serialization is simpler, faster, and more flexible than XML, making it the default choice for modern APIs.
> In .NET, `System.Text.Json` should be preferred for both serialization and deserialization.

---

## 📘 Examples

### Serialization Example (Full Program)

```csharp
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Serialization
{
    /// <summary>
    /// Provides the entry point for the application.
    /// </summary>
    /// <remarks>This class contains the Main method, which serves as the starting point when the application
    /// is executed. It demonstrates serialization of a Student object to JSON format.</remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
        static void Main(string[] args)
        {
            Student s = new Student
            {
                Id = 1,
                Name = "Ali",
                book = new string[] { "Math", "Science", "History" },
                Score = new List<int> { 10, 50, 20, 40 }
            };

            string json = JsonSerializer.Serialize(s);
            Console.WriteLine(json);
        }
    }

    /// <summary>
    /// Represents a student with identifying information, associated books, and a collection of scores.
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] book { get; set; }
        public List<int> Score { get; set; }
    }
}
```

### Deserialization Example (Inline JSON)

```csharp
using System;
using System.Text.Json;

namespace Serialization
{
    public class Program
    {
        static void Main(string[] args)
        {
            string json = @"{
                \"Id\": 1,
                \"Name\": \"Asad Ali\",
                \"book\": [\"Mathematics\", \"Science\", \"History\"],
                \"Score\": [85, 90, 78]
            }";

            Student s = JsonSerializer.Deserialize<Student>(json);

            if (s != null)
            {
                Console.WriteLine(
                    $"ID: {s.Id}, Name: {s.Name}, " +
                    $"Books: {string.Join(", ", s.book)}, " +
                    $"Scores: {string.Join(", ", s.Score)}");

                Console.WriteLine("Deserialization successful!");
            }
            else
            {
                Console.WriteLine("Deserialization failed.");
            }
        }
    }
}
```
