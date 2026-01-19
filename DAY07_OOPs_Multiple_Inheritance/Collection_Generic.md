# Collections and Generics in C#: A Comprehensive Academic Guide

## Table of Contents
1. [Introduction to Collections](#introduction)
2. [Non-Generic Collections](#non-generic-collections)
3. [Generic Collections](#generic-collections)
4. [Generics Fundamentals](#generics-fundamentals)
5. [Advanced Generic Concepts](#advanced-generic-concepts)
6. [Collection Interfaces](#collection-interfaces)
7. [Performance Considerations](#performance-considerations)
8. [Best Practices](#best-practices)

---

## Introduction to Collections {#introduction}

### What are Collections?

Collections are data structures that allow you to store, retrieve, and manipulate groups of related objects. In C#, collections provide:
- Dynamic sizing (unlike arrays)
- Type safety (with generics)
- Rich set of operations (add, remove, search, sort)
- Different organizational structures (lists, stacks, queues, dictionaries)

### Collections Hierarchy

```
System.Collections (Non-Generic)
├── ArrayList
├── Hashtable
├── Stack
├── Queue
└── SortedList

System.Collections.Generic (Generic)
├── List<T>
├── Dictionary<TKey, TValue>
├── Stack<T>
├── Queue<T>
├── HashSet<T>
├── LinkedList<T>
└── SortedDictionary<TKey, TValue>
```

### Why Collections Matter

1. **Flexibility** - Dynamic sizing vs fixed-size arrays
2. **Type Safety** - Generics prevent runtime type errors
3. **Performance** - Optimized for specific use cases
4. **Rich Functionality** - Built-in methods for common operations
5. **Standard Interfaces** - IEnumerable, ICollection, IList

---

## Non-Generic Collections {#non-generic-collections}

Non-generic collections (from `System.Collections`) store objects as `System.Object`, requiring boxing/unboxing and casting. They are less type-safe but provide backward compatibility.

### 1. ArrayList

**Concept:** A dynamically sized array that can store any type of object.

**Characteristics:**
- Stores elements as `object` type
- Automatic resizing
- Zero-based indexing
- Requires type casting when retrieving

**Time Complexity:**
- Access by index: O(1)
- Insert/Remove at end: O(1) amortized
- Insert/Remove at beginning: O(n)
- Search: O(n)

#### Example: ArrayList

```csharp
using System;
using System.Collections;

namespace CollectionsDemo
{
    class ArrayListExample
    {
        static void Main()
        {
            // Creating an ArrayList
            ArrayList myList = new ArrayList();
            
            // Adding different types of elements
            myList.Add(10);           // int
            myList.Add("Hello");      // string
            myList.Add(25.5);         // double
            myList.Add(true);         // bool
            
            Console.WriteLine("=== ArrayList Contents ===");
            foreach (var item in myList)
            {
                Console.WriteLine($"{item} (Type: {item.GetType().Name})");
            }
            
            // Accessing by index (requires casting)
            int firstNumber = (int)myList[0];
            Console.WriteLine($"\nFirst element: {firstNumber}");
            
            // Insert at specific position
            myList.Insert(1, "Inserted");
            
            // Remove element
            myList.Remove("Hello");
            
            // Check if contains
            bool hasTrue = myList.Contains(true);
            Console.WriteLine($"\nContains true: {hasTrue}");
            
            // Get count
            Console.WriteLine($"Count: {myList.Count}");
            Console.WriteLine($"Capacity: {myList.Capacity}");
        }
    }
}
```

**Output:**
```
=== ArrayList Contents ===
10 (Type: Int32)
Hello (Type: String)
25.5 (Type: Double)
True (Type: Boolean)

First element: 10
Contains true: True
Count: 4
Capacity: 4
```

**Problems with ArrayList:**
1. **Type Safety** - Can add any type, leading to runtime errors
2. **Boxing/Unboxing** - Performance overhead for value types
3. **Casting Required** - Must cast when retrieving values

---

### 2. Stack (Non-Generic)

**Concept:** Last-In-First-Out (LIFO) data structure. The last element added is the first one removed.

**Characteristics:**
- LIFO ordering
- Push to add, Pop to remove
- Peek to view top without removing
- Stores objects (requires casting)

**Use Cases:**
- Undo/Redo functionality
- Function call stack
- Expression evaluation
- Backtracking algorithms

#### Example: Stack

```csharp
using System;
using System.Collections;

namespace CollectionsDemo
{
    class StackExample
    {
        static void Main()
        {
            // Creating a Stack
            Stack myStack = new Stack();
            
            // Pushing elements
            myStack.Push(100);       // First
            myStack.Push("World");   // Second
            myStack.Push(50.5);      // Third (top)
            
            Console.WriteLine("=== Stack Operations ===");
            Console.WriteLine($"Count: {myStack.Count}");
            
            // Peek at top element without removing
            object topElement = myStack.Peek();
            Console.WriteLine($"Top element (Peek): {topElement}");
            Console.WriteLine($"Count after Peek: {myStack.Count}");
            
            // Pop elements (LIFO order)
            Console.WriteLine("\n=== Popping Elements ===");
            while (myStack.Count > 0)
            {
                object item = myStack.Pop();
                Console.WriteLine($"Popped: {item} (Type: {item.GetType().Name})");
            }
            
            Console.WriteLine($"\nCount after all pops: {myStack.Count}");
            
            // Safe Pop with type checking
            myStack.Push(100);
            myStack.Push("World");
            myStack.Push(50.5);
            
            Console.WriteLine("\n=== Safe Popping ===");
            if (myStack.Count > 0)
            {
                object poppedItem = myStack.Pop();
                if (poppedItem is int)
                {
                    int intValue = (int)poppedItem;
                    Console.WriteLine($"Popped int: {intValue}");
                }
                else
                {
                    Console.WriteLine($"Popped non-int: {poppedItem}");
                }
            }
        }
    }
}
```

**Output:**
```
=== Stack Operations ===
Count: 3
Top element (Peek): 50.5
Count after Peek: 3

=== Popping Elements ===
Popped: 50.5 (Type: Double)
Popped: World (Type: String)
Popped: 100 (Type: Int32)

Count after all pops: 0

=== Safe Popping ===
Popped non-int: 50.5
```

**Stack Methods:**
- `Push(object)` - Add to top
- `Pop()` - Remove and return top
- `Peek()` - View top without removing
- `Contains(object)` - Check existence
- `Clear()` - Remove all elements

---

### 3. Queue (Non-Generic)

**Concept:** First-In-First-Out (FIFO) data structure. The first element added is the first one removed.

**Characteristics:**
- FIFO ordering
- Enqueue to add, Dequeue to remove
- Peek to view front without removing
- Stores objects (requires casting)

**Use Cases:**
- Task scheduling
- Message queues
- Breadth-first search
- Print spooling

#### Example: Queue

```csharp
using System;
using System.Collections;

namespace CollectionsDemo
{
    class QueueExample
    {
        static void Main()
        {
            // Creating a Queue
            Queue myQueue = new Queue();
            
            // Enqueuing elements
            myQueue.Enqueue(200);      // First (front)
            myQueue.Enqueue("Queue");  // Second
            myQueue.Enqueue(75.5);     // Third (rear)
            
            Console.WriteLine("=== Queue Operations ===");
            Console.WriteLine($"Count: {myQueue.Count}");
            
            // Peek at front element without removing
            object frontElement = myQueue.Peek();
            Console.WriteLine($"Front element (Peek): {frontElement}");
            
            // Dequeue elements (FIFO order)
            Console.WriteLine("\n=== Dequeuing Elements ===");
            while (myQueue.Count > 0)
            {
                object item = myQueue.Dequeue();
                Console.WriteLine($"Dequeued: {item} (Type: {item.GetType().Name})");
            }
            
            Console.WriteLine($"\nCount after all dequeues: {myQueue.Count}");
            
            // Practical Example: Task Queue
            Console.WriteLine("\n=== Task Queue Example ===");
            Queue taskQueue = new Queue();
            taskQueue.Enqueue("Task 1: Send Email");
            taskQueue.Enqueue("Task 2: Update Database");
            taskQueue.Enqueue("Task 3: Generate Report");
            
            while (taskQueue.Count > 0)
            {
                string task = (string)taskQueue.Dequeue();
                Console.WriteLine($"Processing: {task}");
            }
        }
    }
}
```

**Output:**
```
=== Queue Operations ===
Count: 3
Front element (Peek): 200

=== Dequeuing Elements ===
Dequeued: 200 (Type: Int32)
Dequeued: Queue (Type: String)
Dequeued: 75.5 (Type: Double)

Count after all dequeues: 0

=== Task Queue Example ===
Processing: Task 1: Send Email
Processing: Task 2: Update Database
Processing: Task 3: Generate Report
```

**Queue Methods:**
- `Enqueue(object)` - Add to rear
- `Dequeue()` - Remove and return front
- `Peek()` - View front without removing
- `Contains(object)` - Check existence
- `Clear()` - Remove all elements

---

### 4. Hashtable

**Concept:** Stores key-value pairs with fast lookup based on hash codes.

**Characteristics:**
- Key-value storage
- Fast lookup O(1) average
- Keys must be unique
- Not ordered
- Stores objects (requires casting)

#### Example: Hashtable

```csharp
using System;
using System.Collections;

namespace CollectionsDemo
{
    class HashtableExample
    {
        static void Main()
        {
            // Creating a Hashtable
            Hashtable studentGrades = new Hashtable();
            
            // Adding key-value pairs
            studentGrades.Add("Alice", 95);
            studentGrades.Add("Bob", 87);
            studentGrades.Add("Charlie", 92);
            
            // Accessing by key (requires casting)
            int aliceGrade = (int)studentGrades["Alice"];
            Console.WriteLine($"Alice's grade: {aliceGrade}");
            
            // Iterating through Hashtable
            Console.WriteLine("\n=== All Students ===");
            foreach (DictionaryEntry entry in studentGrades)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            
            // Check if key exists
            if (studentGrades.ContainsKey("Bob"))
            {
                Console.WriteLine($"\nBob's grade: {studentGrades["Bob"]}");
            }
            
            // Remove an entry
            studentGrades.Remove("Charlie");
            Console.WriteLine($"\nCount after removing Charlie: {studentGrades.Count}");
        }
    }
}
```

---

### Problems with Non-Generic Collections

1. **Type Safety Issues:**
```csharp
ArrayList list = new ArrayList();
list.Add(10);
list.Add("Hello");
// Runtime error if you assume all are integers
int sum = 0;
foreach (var item in list)
{
    sum += (int)item;  // Throws InvalidCastException
}
```

2. **Boxing/Unboxing Overhead:**
```csharp
ArrayList numbers = new ArrayList();
numbers.Add(10);  // Boxing: int → object
int value = (int)numbers[0];  // Unboxing: object → int
```

3. **No Compile-Time Type Checking:**
```csharp
Stack stack = new Stack();
stack.Push(100);
string wrongType = (string)stack.Pop();  // Compiles, but runtime error
```

**Solution: Generic Collections**

---

## Generic Collections {#generic-collections}

Generic collections (from `System.Collections.Generic`) provide type safety, eliminate boxing/unboxing, and offer better performance.

### 1. List&lt;T&gt;

**Concept:** Generic version of ArrayList. Strongly-typed dynamic array.

**Characteristics:**
- Type-safe
- No boxing/unboxing for value types
- Zero-based indexing
- Automatic resizing
- Best general-purpose collection

**Type Parameter:** `T` - The type of elements in the list

#### Example: List&lt;T&gt; Comprehensive

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class ListExample
    {
        static void Main()
        {
            // Creating a strongly-typed List
            List<int> numbers = new List<int>();
            
            // Adding elements
            numbers.Add(10);
            numbers.Add(20);
            numbers.Add(30);
            // numbers.Add("Hello");  // Compile-time error!
            
            Console.WriteLine("=== List<int> Operations ===");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);  // No casting needed
            }
            
            // Various operations
            numbers.Insert(1, 15);  // Insert at index 1
            numbers.Remove(20);     // Remove by value
            numbers.RemoveAt(0);    // Remove by index
            
            // Searching
            bool contains25 = numbers.Contains(25);
            int indexOf30 = numbers.IndexOf(30);
            
            Console.WriteLine($"\nContains 25: {contains25}");
            Console.WriteLine($"Index of 30: {indexOf30}");
            
            // List with custom objects
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice", Grade = 95 },
                new Student { Id = 2, Name = "Bob", Grade = 87 },
                new Student { Id = 3, Name = "Charlie", Grade = 92 }
            };
            
            Console.WriteLine("\n=== Students List ===");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}: {student.Grade}");
            }
            
            // LINQ operations
            var highPerformers = students.FindAll(s => s.Grade >= 90);
            Console.WriteLine("\n=== High Performers (>=90) ===");
            foreach (var student in highPerformers)
            {
                Console.WriteLine(student.Name);
            }
            
            // Sorting
            students.Sort((a, b) => a.Grade.CompareTo(b.Grade));
            Console.WriteLine("\n=== Sorted by Grade ===");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}: {student.Grade}");
            }
        }
    }
    
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
    }
}
```

**Output:**
```
=== List<int> Operations ===
10
20
30

Contains 25: False
Index of 30: 2

=== Students List ===
Alice: 95
Bob: 87
Charlie: 92

=== High Performers (>=90) ===
Alice
Charlie

=== Sorted by Grade ===
Bob: 87
Charlie: 92
Alice: 95
```

**List&lt;T&gt; Important Methods:**

| Method | Description | Time Complexity |
|--------|-------------|-----------------|
| `Add(T)` | Add to end | O(1) amortized |
| `Insert(int, T)` | Insert at index | O(n) |
| `Remove(T)` | Remove first occurrence | O(n) |
| `RemoveAt(int)` | Remove at index | O(n) |
| `Contains(T)` | Check existence | O(n) |
| `IndexOf(T)` | Find index | O(n) |
| `Sort()` | Sort list | O(n log n) |
| `Clear()` | Remove all | O(n) |
| `Count` | Get count | O(1) |

---

### 2. Dictionary&lt;TKey, TValue&gt;

**Concept:** Generic hash table storing key-value pairs with fast O(1) lookup.

**Characteristics:**
- Type-safe key-value storage
- Unique keys required
- Fast lookups
- Not ordered
- Best for lookup-heavy operations

**Type Parameters:** 
- `TKey` - Type of keys
- `TValue` - Type of values

#### Example: Dictionary&lt;TKey, TValue&gt; Comprehensive

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class DictionaryExample
    {
        static void Main()
        {
            // Creating a Dictionary
            Dictionary<string, int> studentGrades = new Dictionary<string, int>();
            
            // Adding entries
            studentGrades.Add("Alice", 95);
            studentGrades.Add("Bob", 87);
            studentGrades.Add("Charlie", 92);
            // studentGrades.Add("Alice", 98);  // Runtime error: duplicate key
            
            // Accessing values
            int aliceGrade = studentGrades["Alice"];
            Console.WriteLine($"Alice's grade: {aliceGrade}");
            
            // Safe access with TryGetValue
            if (studentGrades.TryGetValue("David", out int davidGrade))
            {
                Console.WriteLine($"David's grade: {davidGrade}");
            }
            else
            {
                Console.WriteLine("David not found");
            }
            
            // Iterating
            Console.WriteLine("\n=== All Students ===");
            foreach (KeyValuePair<string, int> kvp in studentGrades)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            
            // Alternative iteration
            Console.WriteLine("\n=== Using Keys and Values ===");
            foreach (string name in studentGrades.Keys)
            {
                Console.WriteLine($"{name}: {studentGrades[name]}");
            }
            
            // Update value
            studentGrades["Bob"] = 90;  // Updates existing value
            
            // Dictionary with custom objects
            Dictionary<int, Product> products = new Dictionary<int, Product>
            {
                { 101, new Product { Id = 101, Name = "Laptop", Price = 999.99m } },
                { 102, new Product { Id = 102, Name = "Mouse", Price = 29.99m } },
                { 103, new Product { Id = 103, Name = "Keyboard", Price = 79.99m } }
            };
            
            Console.WriteLine("\n=== Products Dictionary ===");
            foreach (var kvp in products)
            {
                Console.WriteLine($"ID {kvp.Key}: {kvp.Value.Name} - ${kvp.Value.Price}");
            }
            
            // Check if key exists
            if (products.ContainsKey(102))
            {
                Console.WriteLine($"\nProduct 102: {products[102].Name}");
            }
            
            // Remove entry
            studentGrades.Remove("Charlie");
            Console.WriteLine($"\nStudent count: {studentGrades.Count}");
        }
    }
    
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

**Output:**
```
Alice's grade: 95
David not found

=== All Students ===
Alice: 95
Bob: 87
Charlie: 92

=== Using Keys and Values ===
Alice: 95
Bob: 87
Charlie: 92

=== Products Dictionary ===
ID 101: Laptop - $999.99
ID 102: Mouse - $29.99
ID 103: Keyboard - $79.99

Product 102: Mouse

Student count: 2
```

**Dictionary&lt;TKey, TValue&gt; Important Methods:**

| Method | Description | Time Complexity |
|--------|-------------|-----------------|
| `Add(TKey, TValue)` | Add key-value pair | O(1) average |
| `Remove(TKey)` | Remove by key | O(1) average |
| `TryGetValue(TKey, out TValue)` | Safe get | O(1) average |
| `ContainsKey(TKey)` | Check key exists | O(1) average |
| `ContainsValue(TValue)` | Check value exists | O(n) |
| `Clear()` | Remove all | O(n) |
| `Keys` | Get all keys | O(1) property |
| `Values` | Get all values | O(1) property |

---

### 3. Stack&lt;T&gt;

**Concept:** Generic LIFO collection.

#### Example: Stack&lt;T&gt;

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class GenericStackExample
    {
        static void Main()
        {
            // Creating a generic Stack
            Stack<int> numbers = new Stack<int>();
            
            // Pushing elements
            numbers.Push(10);
            numbers.Push(20);
            numbers.Push(30);
            
            Console.WriteLine("=== Stack<int> Operations ===");
            Console.WriteLine($"Count: {numbers.Count}");
            Console.WriteLine($"Peek: {numbers.Peek()}");  // No casting!
            
            // Popping all elements
            while (numbers.Count > 0)
            {
                int value = numbers.Pop();  // Type-safe
                Console.WriteLine($"Popped: {value}");
            }
            
            // Practical Example: Undo functionality
            Console.WriteLine("\n=== Undo Example ===");
            Stack<string> undoStack = new Stack<string>();
            
            // Simulate actions
            undoStack.Push("Action 1: Created file");
            undoStack.Push("Action 2: Added text");
            undoStack.Push("Action 3: Formatted text");
            
            Console.WriteLine("Performing undo operations:");
            while (undoStack.Count > 0)
            {
                string action = undoStack.Pop();
                Console.WriteLine($"Undoing: {action}");
            }
        }
    }
}
```

---

### 4. Queue&lt;T&gt;

**Concept:** Generic FIFO collection.

#### Example: Queue&lt;T&gt;

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class GenericQueueExample
    {
        static void Main()
        {
            // Creating a generic Queue
            Queue<string> taskQueue = new Queue<string>();
            
            // Enqueuing tasks
            taskQueue.Enqueue("Task 1: Compile code");
            taskQueue.Enqueue("Task 2: Run tests");
            taskQueue.Enqueue("Task 3: Deploy application");
            
            Console.WriteLine("=== Queue<string> Operations ===");
            Console.WriteLine($"Count: {taskQueue.Count}");
            Console.WriteLine($"Peek: {taskQueue.Peek()}");
            
            // Processing all tasks
            Console.WriteLine("\n=== Processing Tasks ===");
            while (taskQueue.Count > 0)
            {
                string task = taskQueue.Dequeue();  // Type-safe
                Console.WriteLine($"Processing: {task}");
            }
            
            // Practical Example: Customer service queue
            Console.WriteLine("\n=== Customer Service Queue ===");
            Queue<Customer> customerQueue = new Queue<Customer>();
            
            customerQueue.Enqueue(new Customer { Id = 1, Name = "Alice" });
            customerQueue.Enqueue(new Customer { Id = 2, Name = "Bob" });
            customerQueue.Enqueue(new Customer { Id = 3, Name = "Charlie" });
            
            while (customerQueue.Count > 0)
            {
                Customer customer = customerQueue.Dequeue();
                Console.WriteLine($"Serving customer: {customer.Name} (ID: {customer.Id})");
            }
        }
    }
    
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```

---

### 5. HashSet&lt;T&gt;

**Concept:** Unordered collection of unique elements with fast lookup.

**Characteristics:**
- No duplicate elements
- Fast O(1) add, remove, contains operations
- Not ordered
- Set operations (union, intersection, difference)

**Use Cases:**
- Removing duplicates
- Membership testing
- Set operations

#### Example: HashSet&lt;T&gt;

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class HashSetExample
    {
        static void Main()
        {
            // Creating a HashSet
            HashSet<int> uniqueNumbers = new HashSet<int>();
            
            // Adding elements
            uniqueNumbers.Add(10);
            uniqueNumbers.Add(20);
            uniqueNumbers.Add(10);  // Duplicate - ignored
            uniqueNumbers.Add(30);
            
            Console.WriteLine("=== HashSet<int> (No Duplicates) ===");
            foreach (int num in uniqueNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine($"Count: {uniqueNumbers.Count}");  // 3, not 4
            
            // Fast membership testing
            bool contains20 = uniqueNumbers.Contains(20);  // O(1)
            Console.WriteLine($"\nContains 20: {contains20}");
            
            // Set operations
            HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
            HashSet<int> setB = new HashSet<int> { 4, 5, 6, 7, 8 };
            
            // Union
            HashSet<int> union = new HashSet<int>(setA);
            union.UnionWith(setB);
            Console.WriteLine("\n=== Union (A ∪ B) ===");
            Console.WriteLine(string.Join(", ", union));  // 1,2,3,4,5,6,7,8
            
            // Intersection
            HashSet<int> intersection = new HashSet<int>(setA);
            intersection.IntersectWith(setB);
            Console.WriteLine("\n=== Intersection (A ∩ B) ===");
            Console.WriteLine(string.Join(", ", intersection));  // 4,5
            
            // Difference
            HashSet<int> difference = new HashSet<int>(setA);
            difference.ExceptWith(setB);
            Console.WriteLine("\n=== Difference (A - B) ===");
            Console.WriteLine(string.Join(", ", difference));  // 1,2,3
            
            // Symmetric Difference
            HashSet<int> symmetricDiff = new HashSet<int>(setA);
            symmetricDiff.SymmetricExceptWith(setB);
            Console.WriteLine("\n=== Symmetric Difference (A Δ B) ===");
            Console.WriteLine(string.Join(", ", symmetricDiff));  // 1,2,3,6,7,8
            
            // Practical Example: Removing duplicates
            List<string> namesWithDuplicates = new List<string> 
            { 
                "Alice", "Bob", "Charlie", "Alice", "David", "Bob" 
            };
            
            HashSet<string> uniqueNames = new HashSet<string>(namesWithDuplicates);
            Console.WriteLine("\n=== Unique Names ===");
            foreach (string name in uniqueNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
```

**Output:**
```
=== HashSet<int> (No Duplicates) ===
10
20
30
Count: 3

Contains 20: True

=== Union (A ∪ B) ===
1, 2, 3, 4, 5, 6, 7, 8

=== Intersection (A ∩ B) ===
4, 5

=== Difference (A - B) ===
1, 2, 3

=== Symmetric Difference (A Δ B) ===
1, 2, 3, 6, 7, 8

=== Unique Names ===
Alice
Bob
Charlie
David
```

---

### 6. LinkedList&lt;T&gt;

**Concept:** Doubly-linked list with efficient insertion/deletion at any position.

**Characteristics:**
- Not contiguous in memory
- O(1) insertion/deletion at any position (if you have the node)
- O(n) access by index
- Each node has previous and next references

**Use Cases:**
- Frequent insertions/deletions in middle
- Implementing LRU cache
- When you need bidirectional traversal

#### Example: LinkedList&lt;T&gt;

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class LinkedListExample
    {
        static void Main()
        {
            // Creating a LinkedList
            LinkedList<string> playlist = new LinkedList<string>();
            
            // Adding nodes
            playlist.AddLast("Song 1");
            playlist.AddLast("Song 2");
            playlist.AddLast("Song 3");
            playlist.AddFirst("Song 0");  // Add at beginning
            
            Console.WriteLine("=== Playlist ===");
            foreach (string song in playlist)
            {
                Console.WriteLine(song);
            }
            
            // Finding a node
            LinkedListNode<string> node = playlist.Find("Song 2");
            if (node != null)
            {
                // Insert before and after
                playlist.AddBefore(node, "Song 1.5");
                playlist.AddAfter(node, "Song 2.5");
            }
            
            Console.WriteLine("\n=== After Insertions ===");
            foreach (string song in playlist)
            {
                Console.WriteLine(song);
            }
            
            // Traversing forward and backward
            Console.WriteLine("\n=== Forward Traversal ===");
            LinkedListNode<string> current = playlist.First;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
            
            Console.WriteLine("\n=== Backward Traversal ===");
            current = playlist.Last;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Previous;
            }
            
            // Remove nodes
            playlist.Remove("Song 1.5");
            playlist.RemoveFirst();
            playlist.RemoveLast();
            
            Console.WriteLine($"\nCount after removals: {playlist.Count}");
        }
    }
}
```

---

### 7. SortedList&lt;TKey, TValue&gt; and SortedDictionary&lt;TKey, TValue&gt;

**Concept:** Sorted key-value collections.

**Differences:**
- **SortedList** - Uses less memory, faster retrieval, slower insertion O(n)
- **SortedDictionary** - Uses more memory, faster insertion O(log n), slower retrieval

#### Example: SortedDictionary&lt;TKey, TValue&gt;

```csharp
using System;
using System.Collections.Generic;

namespace CollectionsDemo
{
    class SortedCollectionsExample
    {
        static void Main()
        {
            // Sorted Dictionary - keeps keys in sorted order
            SortedDictionary<string, int> scores = new SortedDictionary<string, int>();
            
            scores.Add("Charlie", 92);
            scores.Add("Alice", 95);
            scores.Add("Bob", 87);
            
            Console.WriteLine("=== SortedDictionary (Alphabetical by Key) ===");
            foreach (var kvp in scores)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            
            // Sorted Set - keeps elements in sorted order
            SortedSet<int> sortedNumbers = new SortedSet<int> { 50, 30, 70, 10, 90 };
            
            Console.WriteLine("\n=== SortedSet<int> ===");
            foreach (int num in sortedNumbers)
            {
                Console.WriteLine(num);  // Automatically sorted
            }
        }
    }
}
```

---

## Generics Fundamentals {#generics-fundamentals}

### What are Generics?

**Generics** allow you to define type-safe classes, interfaces, and methods with placeholder types that are specified at instantiation time.

### Benefits of Generics

1. **Type Safety** - Compile-time type checking
2. **Code Reusability** - Write once, use with any type
3. **Performance** - No boxing/unboxing for value types
4. **Cleaner Code** - No casting required

### Generic Class Syntax

```csharp
public class GenericClass<T>
{
    private T field;
    
    public void Method(T parameter)
    {
        field = parameter;
    }
    
    public T GetValue()
    {
        return field;
    }
}
```

---

### Creating Custom Generic Classes

#### Example: Generic Box&lt;T&gt;

```csharp
using System;

namespace GenericsDemo
{
    // Generic class with single type parameter
    public class Box<T>
    {
        private T _content;
        
        public Box(T content)
        {
            _content = content;
        }
        
        public T Content
        {
            get { return _content; }
            set { _content = value; }
        }
        
        public void Display()
        {
            Console.WriteLine($"Box contains: {_content} (Type: {typeof(T).Name})");
        }
    }
    
    // Generic class with multiple type parameters
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }
        
        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }
        
        public void Display()
        {
            Console.WriteLine($"Pair: ({First}, {Second})");
        }
    }
    
    class Program
    {
        static void Main()
        {
            // Using Box with different types
            Box<int> intBox = new Box<int>(123);
            intBox.Display();
            
            Box<string> stringBox = new Box<string>("Hello");
            stringBox.Display();
            
            Box<double> doubleBox = new Box<double>(3.14);
            doubleBox.Display();
            
            // Using Pair with different type combinations
            Console.WriteLine("\n=== Pairs ===");
            Pair<string, int> nameAge = new Pair<string, int>("Alice", 25);
            nameAge.Display();
            
            Pair<int, string> idName = new Pair<int, string>(101, "Bob");
            idName.Display();
            
            Pair<double, double> coordinates = new Pair<double, double>(10.5, 20.3);
            coordinates.Display();
        }
    }
}
```

**Output:**
```
Box contains: 123 (Type: Int32)
Box contains: Hello (Type: String)
Box contains: 3.14 (Type: Double)

=== Pairs ===
Pair: (Alice, 25)
Pair: (101, Bob)
Pair: (10.5, 20.3)
```

---

### Generic Methods

You can create generic methods in non-generic classes:

```csharp
using System;

namespace GenericsDemo
{
    class Utilities
    {
        // Generic method to swap two values
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        
        // Generic method to print array
        public static void PrintArray<T>(T[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("]");
        }
        
        // Generic method to find maximum
        public static T FindMax<T>(T[] array) where T : IComparable<T>
        {
            if (array.Length == 0)
                throw new ArgumentException("Array cannot be empty");
                
            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                    max = array[i];
            }
            return max;
        }
    }
    
    class Program
    {
        static void Main()
        {
            // Using generic Swap method
            Console.WriteLine("=== Swap Method ===");
            int x = 5, y = 10;
            Console.WriteLine($"Before: x={x}, y={y}");
            Utilities.Swap(ref x, ref y);
            Console.WriteLine($"After: x={x}, y={y}");
            
            string a = "Hello", b = "World";
            Console.WriteLine($"\nBefore: a={a}, b={b}");
            Utilities.Swap(ref a, ref b);
            Console.WriteLine($"After: a={a}, b={b}");
            
            // Using generic PrintArray method
            Console.WriteLine("\n=== PrintArray Method ===");
            int[] numbers = { 1, 2, 3, 4, 5 };
            Utilities.PrintArray(numbers);
            
            string[] words = { "apple", "banana", "cherry" };
            Utilities.PrintArray(words);
            
            // Using generic FindMax method
            Console.WriteLine("\n=== FindMax Method ===");
            int[] values = { 45, 23, 78, 12, 90, 34 };
            int maxValue = Utilities.FindMax(values);
            Console.WriteLine($"Max value: {maxValue}");
            
            string[] names = { "Alice", "Bob", "Charlie", "David" };
            string maxName = Utilities.FindMax(names);
            Console.WriteLine($"Max name (alphabetically): {maxName}");
        }
    }
}
```

**Output:**
```
=== Swap Method ===
Before: x=5, y=10
After: x=10, y=5

Before: a=Hello, b=World
After: a=World, b=Hello

=== PrintArray Method ===
[1, 2, 3, 4, 5]
[apple, banana, cherry]

=== FindMax Method ===
Max value: 90
Max name (alphabetically): David
```

---

## Advanced Generic Concepts {#advanced-generic-concepts}

### Generic Constraints

Constraints restrict what types can be used as type arguments.

**Types of Constraints:**

1. **`where T : struct`** - T must be a value type
2. **`where T : class`** - T must be a reference type
3. **`where T : new()`** - T must have a parameterless constructor
4. **`where T : BaseClass`** - T must be or derive from BaseClass
5. **`where T : Interface`** - T must implement Interface
6. **`where T : U`** - T must be or derive from U (another type parameter)

#### Example: Generic Constraints

```csharp
using System;
using System.Collections.Generic;

namespace GenericsDemo
{
    // Constraint: T must be a reference type
    public class ReferenceTypeContainer<T> where T : class
    {
        private T _item;
        
        public void SetItem(T item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }
        
        public bool IsNull()
        {
            return _item == null;
        }
    }
    
    // Constraint: T must be a value type
    public class ValueTypeContainer<T> where T : struct
    {
        private T _value;
        
        public ValueTypeContainer(T value)
        {
            _value = value;
        }
        
        public T GetValue()
        {
            return _value;
        }
    }
    
    // Constraint: T must have a parameterless constructor
    public class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();  // Can use new() because of constraint
        }
    }
    
    // Constraint: T must implement an interface
    public class Comparer<T> where T : IComparable<T>
    {
        public T GetLarger(T first, T second)
        {
            return first.CompareTo(second) > 0 ? first : second;
        }
    }
    
    // Multiple constraints
    public class AdvancedContainer<T> where T : class, IDisposable, new()
    {
        private T _instance;
        
        public T GetOrCreate()
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
        
        public void Cleanup()
        {
            _instance?.Dispose();
            _instance = null;
        }
    }
    
    // Classes for demonstration
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    
    public class DisposableResource : IDisposable
    {
        public bool IsDisposed { get; private set; }
        
        public void Dispose()
        {
            IsDisposed = true;
            Console.WriteLine("Resource disposed");
        }
    }
    
    class Program
    {
        static void Main()
        {
            // Using reference type constraint
            Console.WriteLine("=== Reference Type Constraint ===");
            ReferenceTypeContainer<string> stringContainer = new ReferenceTypeContainer<string>();
            stringContainer.SetItem("Hello");
            Console.WriteLine($"Is null: {stringContainer.IsNull()}");
            // ReferenceTypeContainer<int> intContainer; // Error: int is value type
            
            // Using value type constraint
            Console.WriteLine("\n=== Value Type Constraint ===");
            ValueTypeContainer<int> intContainer = new ValueTypeContainer<int>(42);
            Console.WriteLine($"Value: {intContainer.GetValue()}");
            // ValueTypeContainer<string> strContainer; // Error: string is reference type
            
            // Using constructor constraint
            Console.WriteLine("\n=== Constructor Constraint ===");
            Factory<Person> personFactory = new Factory<Person>();
            Person person = personFactory.CreateInstance();
            person.Name = "Alice";
            Console.WriteLine($"Created person: {person.Name}");
            
            // Using interface constraint
            Console.WriteLine("\n=== Interface Constraint ===");
            Comparer<int> intComparer = new Comparer<int>();
            int larger = intComparer.GetLarger(10, 20);
            Console.WriteLine($"Larger number: {larger}");
            
            Comparer<string> stringComparer = new Comparer<string>();
            string largerString = stringComparer.GetLarger("apple", "banana");
            Console.WriteLine($"Larger string: {largerString}");
            
            // Using multiple constraints
            Console.WriteLine("\n=== Multiple Constraints ===");
            AdvancedContainer<DisposableResource> container = 
                new AdvancedContainer<DisposableResource>();
            DisposableResource resource = container.GetOrCreate();
            Console.WriteLine($"Resource disposed: {resource.IsDisposed}");
            container.Cleanup();
            Console.WriteLine($"After cleanup - Resource disposed: {resource.IsDisposed}");
        }
    }
}
```

---

### Covariance and Contravariance

**Covariance** allows you to use a more derived type than originally specified (out).  
**Contravariance** allows you to use a less derived type (in).

#### Example: Covariance and Contravariance

```csharp
using System;
using System.Collections.Generic;

namespace GenericsDemo
{
    // Base and derived classes
    class Animal
    {
        public string Name { get; set; }
        public virtual void MakeSound()
        {
            Console.WriteLine("Some sound");
        }
    }
    
    class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof!");
        }
    }
    
    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow!");
        }
    }
    
    // Covariant interface (out keyword)
    interface IProducer<out T>
    {
        T Produce();
    }
    
    class DogProducer : IProducer<Dog>
    {
        public Dog Produce()
        {
            return new Dog { Name = "Buddy" };
        }
    }
    
    // Contravariant interface (in keyword)
    interface IConsumer<in T>
    {
        void Consume(T item);
    }
    
    class AnimalConsumer : IConsumer<Animal>
    {
        public void Consume(Animal animal)
        {
            Console.WriteLine($"Processing {animal.Name}");
            animal.MakeSound();
        }
    }
    
    class Program
    {
        static void Main()
        {
            // Covariance example
            Console.WriteLine("=== Covariance (out) ===");
            IProducer<Dog> dogProducer = new DogProducer();
            // Covariance: IProducer<Dog> can be assigned to IProducer<Animal>
            IProducer<Animal> animalProducer = dogProducer;
            Animal animal = animalProducer.Produce();
            animal.MakeSound();
            
            // Covariance with IEnumerable
            IEnumerable<Dog> dogs = new List<Dog> 
            { 
                new Dog { Name = "Max" }, 
                new Dog { Name = "Bella" } 
            };
            // IEnumerable<Dog> can be assigned to IEnumerable<Animal>
            IEnumerable<Animal> animals = dogs;
            foreach (Animal a in animals)
            {
                Console.WriteLine(a.Name);
            }
            
            // Contravariance example
            Console.WriteLine("\n=== Contravariance (in) ===");
            IConsumer<Animal> animalConsumer = new AnimalConsumer();
            // Contravariance: IConsumer<Animal> can be assigned to IConsumer<Dog>
            IConsumer<Dog> dogConsumer = animalConsumer;
            dogConsumer.Consume(new Dog { Name = "Charlie" });
        }
    }
}
```

---

## Collection Interfaces {#collection-interfaces}

Understanding collection interfaces is crucial for working with collections effectively.

### Main Interfaces

1. **`IEnumerable<T>`** - Supports simple iteration
2. **`ICollection<T>`** - Adds Count, Add, Remove, Clear
3. **`IList<T>`** - Adds indexed access and insertion
4. **`IDictionary<TKey, TValue>`** - Key-value pairs

### Interface Hierarchy

```
IEnumerable<T>
    ↓
ICollection<T>
    ↓
IList<T> ───→ List<T>
    ↓
    ↓
IDictionary<TKey,TValue> ───→ Dictionary<TKey, TValue>
```

#### Example: Using Interfaces

```csharp
using System;
using System.Collections.Generic;

namespace InterfacesDemo
{
    class Program
    {
        static void Main()
        {
            // IEnumerable - read-only iteration
            IEnumerable<int> enumerable = new List<int> { 1, 2, 3, 4, 5 };
            PrintNumbers("IEnumerable", enumerable);
            // enumerable.Add(6); // Error: IEnumerable doesn't have Add
            
            // ICollection - adds Count, Add, Remove
            ICollection<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine($"\nICollection Count: {collection.Count}");
            collection.Add(6);
            collection.Remove(1);
            PrintNumbers("ICollection (after modifications)", collection);
            // int value = collection[0]; // Error: no indexer
            
            // IList - adds indexed access
            IList<int> list = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine($"\nIList element at index 2: {list[2]}");
            list.Insert(0, 0);
            PrintNumbers("IList (after insert)", list);
            
            // IDictionary - key-value pairs
            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "Alice", 25 },
                { "Bob", 30 },
                { "Charlie", 35 }
            };
            Console.WriteLine($"\nIDictionary - Alice's age: {dictionary["Alice"]}");
            dictionary.Add("David", 40);
            Console.WriteLine($"Dictionary count: {dictionary.Count}");
        }
        
        static void PrintNumbers(string label, IEnumerable<int> numbers)
        {
            Console.Write($"{label}: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }
    }
}
```

---

## Performance Considerations {#performance-considerations}

### Time Complexity Comparison

| Operation | List&lt;T&gt; | Dictionary&lt;K,V&gt; | HashSet&lt;T&gt; | LinkedList&lt;T&gt; |
|-----------|--------------|----------------------|-----------------|-------------------|
| **Add** (at end) | O(1) amortized | O(1) average | O(1) average | O(1) |
| **Insert** (at position) | O(n) | N/A | N/A | O(1) if have node |
| **Remove** (by value) | O(n) | O(1) average | O(1) average | O(n) |
| **Access** (by index/key) | O(1) | O(1) average | N/A | O(n) |
| **Contains** | O(n) | O(1) average | O(1) average | O(n) |
| **Iteration** | O(n) | O(n) | O(n) | O(n) |

### Memory Usage

1. **List&lt;T&gt;** - Contiguous memory, capacity > count
2. **Dictionary&lt;TKey, TValue&gt;** - Higher memory due to hash table
3. **LinkedList&lt;T&gt;** - Extra memory for node pointers
4. **HashSet&lt;T&gt;** - Similar to Dictionary

### Choosing the Right Collection

**Use List&lt;T&gt; when:**
- Need indexed access
- Mostly adding to end
- Order matters
- General-purpose collection

**Use Dictionary&lt;TKey, TValue&gt; when:**
- Key-value associations
- Fast lookups by key needed
- Unique keys

**Use HashSet&lt;T&gt; when:**
- Unique elements only
- Fast membership testing
- Set operations needed

**Use Queue&lt;T&gt; when:**
- FIFO processing
- Task scheduling
- Breadth-first algorithms

**Use Stack&lt;T&gt; when:**
- LIFO processing
- Undo functionality
- Depth-first algorithms

**Use LinkedList&lt;T&gt; when:**
- Frequent insertions/deletions in middle
- Don't need indexed access
- Memory is not a concern

---

## Best Practices {#best-practices}

### 1. Prefer Generic Collections

```csharp
// ❌ Bad: Non-generic
ArrayList list = new ArrayList();
list.Add(10);
int value = (int)list[0];  // Casting required

// ✅ Good: Generic
List<int> list = new List<int>();
list.Add(10);
int value = list[0];  // No casting
```

### 2. Use Appropriate Collection Initialization

```csharp
// Collection initializer
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Dictionary initializer
Dictionary<string, int> scores = new Dictionary<string, int>
{
    { "Alice", 95 },
    { "Bob", 87 }
};

// Specify initial capacity if known
List<int> bigList = new List<int>(1000);
```

### 3. Use Interfaces for Method Parameters

```csharp
// ✅ Good: Flexible
public void ProcessItems(IEnumerable<int> items)
{
    foreach (int item in items)
    {
        // Process
    }
}

// ❌ Less flexible: Tied to specific implementation
public void ProcessItems(List<int> items)
{
    // Only accepts List<int>
}
```

### 4. Choose Correct Collection Type

```csharp
// Need fast lookups? Use Dictionary
Dictionary<int, Customer> customerById = new Dictionary<int, Customer>();

// Need unique items? Use HashSet
HashSet<string> uniqueEmails = new HashSet<string>();

// Need ordered items? Use List
List<Order> orders = new List<Order>();

// Need FIFO? Use Queue
Queue<Task> taskQueue = new Queue<Task>();
```

### 5. Use LINQ for Queries

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Filter
var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();

// Transform
var doubled = numbers.Select(n => n * 2).ToList();

// Aggregate
int sum = numbers.Sum();
int max = numbers.Max();
```

### 6. Be Careful with Collection Modifications During Iteration

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// ❌ Bad: Modifying during iteration
foreach (int num in numbers)
{
    if (num % 2 == 0)
        numbers.Remove(num);  // Throws InvalidOperationException
}

// ✅ Good: Create new list or use for loop
List<int> toRemove = numbers.Where(n => n % 2 == 0).ToList();
foreach (int num in toRemove)
{
    numbers.Remove(num);
}

// Or use RemoveAll
numbers.RemoveAll(n => n % 2 == 0);
```

### 7. Use TryGetValue with Dictionary

```csharp
Dictionary<string, int> scores = new Dictionary<string, int>();

// ❌ Bad: May throw exception
if (scores.ContainsKey("Alice"))
{
    int score = scores["Alice"];
}

// ✅ Good: Single lookup
if (scores.TryGetValue("Alice", out int score))
{
    // Use score
}
```

---

## Summary

### Key Takeaways

1. **Collections** provide dynamic, flexible data structures
2. **Generic collections** offer type safety and better performance
3. **Choose the right collection** based on your use case:
   - `List<T>` for general-purpose
   - `Dictionary<K,V>` for key-value lookups
   - `HashSet<T>` for unique elements
   - `Queue<T>` for FIFO
   - `Stack<T>` for LIFO
4. **Generics** enable type-safe, reusable code
5. **Constraints** allow you to restrict and enable operations on generic types
6. **Understand time complexity** to make informed decisions
7. **Use interfaces** for flexibility in method parameters

### Further Reading

- [MSDN Collections Documentation](https://docs.microsoft.com/en-us/dotnet/standard/collections/)
- [Generic Type Parameters](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/)
- [LINQ to Objects](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)

---

## Practice Exercises

### Exercise 1: Build a Student Management System
Create a system using `Dictionary<int, Student>` to store students by ID, implement add, remove, and search functionality.

### Exercise 2: Implement a Generic Stack
Build your own generic `MyStack<T>` class with Push, Pop, and Peek methods.

### Exercise 3: Remove Duplicates
Given a `List<int>` with duplicates, use `HashSet<T>` to remove duplicates while maintaining original order.

### Exercise 4: Task Queue System
Implement a task processing system using `Queue<Task>` that processes tasks in FIFO order.

### Exercise 5: LRU Cache
Implement a Least Recently Used cache using `LinkedList<T>` and `Dictionary<K,V>`.

---

This comprehensive guide covers all essential concepts of Collections and Generics in C#. Master these fundamentals to write efficient, type-safe, and maintainable code.
