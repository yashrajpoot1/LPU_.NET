# Serialization

# **How to Convert an Object into XML in C# (.NET)**

We’ll go **from absolute basics → real-world usage → enterprise mindset**, and then I’ll give you **3 practice problems (Easy, Medium, Hard)** like an internal company assignment.

---

## 1️⃣ First, Understand the Basics (No Coding Yet)

### What is an Object in C#?

An **object** is an instance of a **class**.

```csharp
class Student
{
    public int Id;
    public string Name;
}
```

```csharp
Student s = new Student();
s.Id = 1;
s.Name = "Ali";
```

Here:

* `Student` = blueprint
* `s` = object (data in memory)

---

### What is XML?

XML (**Extensible Markup Language**) is a **structured, hierarchical text format** used for:

* Data storage
* Data exchange (APIs, configs, legacy systems)

Example XML:

```xml
<Student>
  <Id>1</Id>
  <Name>Ali</Name>
</Student>
```

👉 **Key idea:**
We want to **convert an in-memory C# object → XML text**

![Image](https://procodeguide.com/wp-content/uploads/2023/05/XML-Serialization-in-Charp-NET.png)

![Image](https://www.w3schools.com/xml/nodetree.gif)

---

## 2️⃣ How .NET Thinks About Object → XML

In .NET, this process is called **Serialization**.

> **Serialization** = Converting an object into a format that can be stored or transmitted
> **Deserialization** = Converting it back into an object

For XML, we use:

### ✅ `XmlSerializer` (MOST IMPORTANT)

Namespace:

```csharp
using System.Xml.Serialization;
```

---

## 3️⃣ Your First Working Example (Beginner Level)

### Step 1: Create a Simple Class

**Rule:**
✔ Public class
✔ Public properties
✔ Parameterless constructor (important!)

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

---

### Step 2: Convert Object to XML

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

class Program
{
    static void Main()
    {
        Student s = new Student
        {
            Id = 1,
            Name = "Ali"
        };

        XmlSerializer serializer = new XmlSerializer(typeof(Student));

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, s);
            string xmlOutput = writer.ToString();
            Console.WriteLine(xmlOutput);
        }
    }
}
```

### Output:

```xml
<?xml version="1.0" encoding="utf-16"?>
<Student xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Id>1</Id>
  <Name>Ali</Name>
</Student>
```

🎯 **Congratulations** — you’ve done your first Object → XML conversion.

---

## 4️⃣ Architect-Level Understanding (VERY IMPORTANT)

### Why These Rules Exist?

| Rule                      | Reason                                   |
| ------------------------- | ---------------------------------------- |
| Public class              | Serializer needs access                  |
| Public properties         | Fields are ignored by default            |
| Parameterless constructor | XML serializer creates object internally |
| No private setters        | Serializer cannot assign values          |

---

## 5️⃣ Customizing XML (Real-World Usage)

### Rename XML Elements

```csharp
public class Student
{
    [XmlElement("StudentId")]
    public int Id { get; set; }

    [XmlElement("FullName")]
    public string Name { get; set; }
}
```

### Change Root Element Name

```csharp
[XmlRoot("StudentInfo")]
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

### Result:

```xml
<StudentInfo>
  <Id>1</Id>
  <Name>Ali</Name>
</StudentInfo>
```

---

## 6️⃣ Object with Collection (Industry Scenario)

```csharp
public class Classroom
{
    public string ClassName { get; set; }

    public Student[] Students { get; set; }
}
```

```csharp
Classroom c = new Classroom
{
    ClassName = "CSE-A",
    Students = new Student[]
    {
        new Student { Id = 1, Name = "Ali" },
        new Student { Id = 2, Name = "Rahul" }
    }
};
```

XML Output:

```xml
<Classroom>
  <ClassName>CSE-A</ClassName>
  <Students>
    <Student>
      <Id>1</Id>
      <Name>Ali</Name>
    </Student>
    <Student>
      <Id>2</Id>
      <Name>Rahul</Name>
    </Student>
  </Students>
</Classroom>
```

---

## 7️⃣ Common Mistakes New Joiners Make ❌

❌ Using private fields (becuase of security we used private then why should we exposed it)
❌ Forgetting parameterless constructor
❌ Trying to serialize interfaces
❌ Expecting methods to appear in XML

> ⚠ XML stores **DATA**, not **BEHAVIOR**

---

## 8️⃣ Practice Problems (Company Training Style)

### 🟢 EASY (Fresher Level)

**Problem:**
Create a `Book` class with:

* BookId
* Title
* Price

Convert one `Book` object into XML and print it.

**Expectation:**
✔ Correct XML
✔ Uses `XmlSerializer`

---

### 🟡 MEDIUM (1–2 Years Experience)

**Problem:**
Create an `Employee` class:

* Id
* Name
* Department
* Salary

Requirements:

* Root element name should be `EmployeeRecord`
* Salary XML element should be `MonthlySalary`

**Hint:**
Use `[XmlRoot]` and `[XmlElement]`

---

### 🔴 HARD (Senior / Enterprise Level)

**Problem:**
Design a **Library System** with:

* LibraryName
* List of Books
  Each Book has:
* ISBN
* Title
* Author
* Price

Requirements:

1. XML root must be `LibraryData`
2. Books must appear under `<Books>`
3. Output XML must be written to a **file**, not console
4. No XML namespaces in output

**Hint:**

```csharp
XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
ns.Add("", "");
```

---

## 9️⃣ Final Architect Advice 🧠

* XML is **verbose but powerful**
* Still heavily used in:

  * Banking
  * Government
  * Legacy enterprise systems
* Learn XML **once properly**, it will help in:

  * SOAP APIs
  * XSD validation
  * Config systems
  * Data contracts

---


## 1️⃣ Your Code Review (Senior Architect Feedback)

### Your code

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

```csharp
XmlSerializer serializer = new XmlSerializer(typeof(Student));

using (StringWriter writer = new StringWriter())
{
    serializer.Serialize(writer, s);
    string xmlOutput = writer.ToString();
    Console.WriteLine(xmlOutput);
}
```

### Verdict

✔ Correct
✔ Clean
✔ Beginner-friendly
✔ Great for **learning & interviews**

This is the **FOUNDATION**.
Every other approach is built **on top of this concept**.

---

## 2️⃣ Then Why Seniors Don’t Write This Every Time?

In **real industry projects**, we rarely:

* Serialize in `Main`
* Print XML to console
* Repeat serializer logic everywhere

Instead, we follow **patterns**.

---

## 3️⃣ MOST USED INDUSTRY WAY #1

### ➜ **Utility / Helper Method (VERY COMMON)**

### Why?

* Reusability
* Clean architecture
* Testable code

### Example

```csharp
public static class XmlHelper
{
    public static string ToXml<T>(T data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, data);
            return writer.ToString();
        }
    }
}
```

### Usage

```csharp
Student s = new Student { Id = 1, Name = "Ali" };
string xml = XmlHelper.ToXml(s);
Console.WriteLine(xml);
```

🧠 **Architect Thinking:**

> Business code should NOT care how XML is generated.

## Serialization in .NET (C#) — Structured Notes

This note provides a concise, academic overview of serialization in .NET with practical C# examples. It covers concepts, serializers, attributes, patterns, pitfalls, and best practices.

---

### 1. Overview

- Serialization: Converting an object graph into a transferable/storable representation (text or binary).
- Deserialization: Reconstructing the object graph from that representation.
- Common use cases: persistence (files/DB), inter-process communication, web APIs, caching, configuration, messaging.

---

### 2. Key Terms

- Object graph: The object and everything it references (recursively).
- Contract: The set of members included in serialization (often controlled by attributes/options).
- Round-trip: Serialize → deserialize and obtain an equivalent object.

---

### 3. Serializers in .NET (When to Use)

- XmlSerializer (System.Xml.Serialization)
  - Human-readable XML; good for interop and legacy systems; requires public types/properties and parameterless ctor; ignores private members.
- System.Text.Json (built-in JSON)
  - Fast JSON; ideal for web APIs; supports attributes and options for naming and null handling.
- DataContractSerializer (System.Runtime.Serialization)
  - XML or JSON; attribute-driven contracts; can work with non-public members; useful for service contracts.
- BinaryFormatter
  - Deprecated and insecure. Do not use. Prefer custom binary (e.g., protobuf) or JSON.

---

### 4. XML Serialization with XmlSerializer

Requirements and behavior:
- Public class and public readable/writable properties are serialized by default.
- Parameterless constructor required for deserialization.
- Fields are ignored unless explicitly configured.

Basic example (serialize to string):

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class Demo
{
    public static void Run()
    {
        var s = new Student { Id = 1, Name = "Ali" };
        var serializer = new XmlSerializer(typeof(Student));

        using var writer = new StringWriter();
        serializer.Serialize(writer, s);
        Console.WriteLine(writer.ToString());
    }
}
```

Serialize to file/stream:

```csharp
using var fs = new FileStream("student.xml", FileMode.Create);
var serializer = new XmlSerializer(typeof(Student));
serializer.Serialize(fs, new Student { Id = 1, Name = "Ali" });
```

Control XML shape with attributes:

```csharp
using System.Xml.Serialization;

[XmlRoot("StudentInfo")]
public class Student
{
    [XmlElement("StudentId")] public int Id { get; set; }
    [XmlElement("FullName")] public string Name { get; set; }
}
```

Collections and element names:

```csharp
public class Classroom
{
    public string ClassName { get; set; }

    [XmlArray("Students")]
    [XmlArrayItem("Student")]
    public Student[] Students { get; set; }
}
```

Remove default XML namespaces (common requirement):

```csharp
var ns = new XmlSerializerNamespaces();
ns.Add(string.Empty, string.Empty);
var ser = new XmlSerializer(typeof(Student));
using var w = new StringWriter();
ser.Serialize(w, new Student { Id = 1, Name = "Ali" }, ns);
```

---

### 5. JSON with System.Text.Json

Basic example:

```csharp
using System.Text.Json;

public class Student { public int Id { get; set; } public string Name { get; set; } }

var s = new Student { Id = 1, Name = "Ali" };
var json = JsonSerializer.Serialize(s, new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
});
Console.WriteLine(json);
```

Deserialize:

```csharp
var obj = JsonSerializer.Deserialize<Student>(json);
```

---

### 6. DataContractSerializer (Attribute-Driven)

Useful when explicit control over the contract is required or for service contracts.

```csharp
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

[DataContract]
public class Employee
{
    [DataMember(Order = 1)] public int Id { get; set; }
    [DataMember(Order = 2)] public string Name { get; set; }
    [DataMember(Order = 3)] public string Department { get; set; }
}

var emp = new Employee { Id = 10, Name = "Riya", Department = "IT" };
var dcs = new DataContractSerializer(typeof(Employee));
using var ms = new MemoryStream();
using var xw = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true });
dcs.WriteObject(xw, emp);
xw.Flush();
Console.WriteLine(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
```

---

### 7. Deserialization Basics

XmlSerializer:

```csharp
var serializer = new XmlSerializer(typeof(Student));
using var reader = new StringReader(xmlString);
var student = (Student)serializer.Deserialize(reader);
```

System.Text.Json:

```csharp
var student = JsonSerializer.Deserialize<Student>(jsonString);
```

DataContractSerializer:

```csharp
var dcs = new DataContractSerializer(typeof(Employee));
using var xr = XmlReader.Create(new StringReader(xmlString));
var employee = (Employee)dcs.ReadObject(xr);
```

---

### 8. Common Pitfalls and Limitations

- XmlSerializer requires a public parameterless constructor for deserialization.
- Private fields/properties are ignored by XmlSerializer; use public properties.
- Interfaces and abstract types require concrete types for deserialization (consider known types for DCS).
- Circular references are not supported by XmlSerializer; JSON supports reference handling via options.
- Methods and computed-only members are not serialized unless exposed as properties with getters/setters.

---

### 9. Security Considerations

- Never deserialize untrusted data with types that can cause code execution or resource access.
- Do not use BinaryFormatter (obsolete and insecure).
- Limit depth/size to prevent DoS (e.g., `MaxDepth` in JSON options).
- Validate and sanitize inputs, especially when using polymorphic types.

---

### 10. Versioning and Compatibility

- Prefer additive changes (new optional members with defaults).
- Use `[DefaultValue]`, JSON ignore conditions, or nullable types to handle missing members.
- With DataContractSerializer, use `[DataMember(IsRequired = false)]` and maintain stable `Order`.

---

### 11. Performance Tips

- Reuse serializers where possible (XmlSerializer can be cached per type).
- Prefer `System.Text.Json` for high-throughput web scenarios.
- Stream directly to `Stream`/`PipeWriter` instead of buffering entire strings when large payloads.
- Avoid excessive attribute customization when defaults suffice.

---

### 12. Practice Exercises

1) XML (Beginner): Create `Book { BookId, Title, Price }`. Serialize to XML and print. Add `[XmlRoot("BookInfo")]`.

2) XML (Intermediate): `Employee { Id, Name, Department, Salary }`. Serialize with root `EmployeeRecord` and element name `MonthlySalary` for salary. Write to `employee.xml` without namespaces.

3) JSON (Intermediate): Serialize and deserialize `Order { Id, Date, Items: List<string> }` with camelCase and indented formatting.

4) DataContract (Advanced): `Library { Name, Books: List<Book> }` with `[DataContract]`/`[DataMember(Order=...)]`. Serialize with `DataContractSerializer` to a stream.

---

### 13. Minimal End-to-End XML Example (Consolidated)

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("StudentInfo")]
public class Student
{
    [XmlElement("StudentId")] public int Id { get; set; }
    [XmlElement("FullName")] public string Name { get; set; }
    [XmlArray("Scores"), XmlArrayItem("Score")] public int[] Score { get; set; }
}

public class Program
{
    public static void Main()
    {
        var s = new Student { Id = 1, Name = "Ali", Score = new[] { 10, 20, 40, 80 } };
        var ns = new XmlSerializerNamespaces();
        ns.Add(string.Empty, string.Empty);

        var serializer = new XmlSerializer(typeof(Student));
        using var writer = new StringWriter();
        serializer.Serialize(writer, s, ns);
        Console.WriteLine(writer.ToString());
    }
}
```

---

These notes intentionally avoid repetition, use consistent formatting, and present a practical, academically structured overview suitable for interviews and real-world development.
