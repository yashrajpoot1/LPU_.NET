using System;
using Generics_Assessment.Scenario1;
using Generics_Assessment.Scenario2;
using Generics_Assessment.Scenario3;
using Generics_Assessment.Scenario4;

namespace Generics_Assessment
{
    /// <summary>
    /// Main program to run all Generics & Collections Assessment Scenarios
    /// Date: 16-02-26
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║    SCENARIO-BASED GENERICS & COLLECTIONS ASSESSMENT          ║");
            Console.WriteLine("║                   Date: 16-02-26                             ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            bool continueProgram = true;

            while (continueProgram)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Scenario1Demo.Run();
                        break;

                    case "2":
                        Scenario2Demo.Run();
                        break;

                    case "3":
                        Scenario3Demo.Run();
                        break;

                    case "4":
                        Scenario4Demo.Run();
                        break;

                    case "5":
                        RunAllScenarios();
                        break;

                    case "6":
                        ShowAssessmentCriteria();
                        break;

                    case "0":
                        continueProgram = false;
                        Console.WriteLine("\n✓ Thank you for using the Generics Assessment System!");
                        Console.WriteLine("  All scenarios demonstrate:");
                        Console.WriteLine("  • Type Safety with Generic Constraints");
                        Console.WriteLine("  • Proper Collection Usage");
                        Console.WriteLine("  • Error Handling & Validation");
                        Console.WriteLine("  • Design Patterns (Repository, Strategy, Factory)");
                        Console.WriteLine("  • Performance Optimization");
                        Console.WriteLine("\nGoodbye!");
                        break;

                    default:
                        Console.WriteLine("✗ Invalid choice. Please try again.");
                        break;
                }

                if (continueProgram && choice != "5")
                {
                    Console.WriteLine("\n" + new string('═', 60));
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MAIN MENU                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  1. Scenario 1: E-Commerce Inventory System");
            Console.WriteLine("     └─ Generic Repository, Product Management, Discounts");
            Console.WriteLine();
            Console.WriteLine("  2. Scenario 2: University Course Registration");
            Console.WriteLine("     └─ Multiple Constraints, Enrollment System, GradeBook");
            Console.WriteLine();
            Console.WriteLine("  3. Scenario 3: Hospital Patient Management");
            Console.WriteLine("     └─ Priority Queue, Medical Records, Medication System");
            Console.WriteLine();
            Console.WriteLine("  4. Scenario 4: Financial Trading Platform");
            Console.WriteLine("     └─ Portfolio Management, Trading Strategy, Price History");
            Console.WriteLine();
            Console.WriteLine("  5. Run All Scenarios");
            Console.WriteLine("     └─ Execute all scenarios sequentially");
            Console.WriteLine();
            Console.WriteLine("  6. Show Assessment Criteria");
            Console.WriteLine("     └─ View evaluation criteria and concepts tested");
            Console.WriteLine();
            Console.WriteLine("  0. Exit");
            Console.WriteLine();
            Console.WriteLine(new string('─', 62));
            Console.Write("  Enter your choice: ");
        }

        static void RunAllScenarios()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              RUNNING ALL SCENARIOS                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            try
            {
                Scenario1Demo.Run();
                Console.WriteLine("\n" + new string('═', 60));
                Console.WriteLine("Press any key to continue to Scenario 2...");
                Console.ReadKey();
                Console.Clear();

                Scenario2Demo.Run();
                Console.WriteLine("\n" + new string('═', 60));
                Console.WriteLine("Press any key to continue to Scenario 3...");
                Console.ReadKey();
                Console.Clear();

                Scenario3Demo.Run();
                Console.WriteLine("\n" + new string('═', 60));
                Console.WriteLine("Press any key to continue to Scenario 4...");
                Console.ReadKey();
                Console.Clear();

                Scenario4Demo.Run();

                Console.WriteLine("\n\n╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║          ALL SCENARIOS COMPLETED SUCCESSFULLY!               ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error running scenarios: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }

        static void ShowAssessmentCriteria()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ASSESSMENT CRITERIA                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            Console.WriteLine("\n┌─ EVALUATION CRITERIA ─────────────────────────────────────┐");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Criterion              Weight    What to Look For        │");
            Console.WriteLine("│  ─────────────────────  ──────    ────────────────────    │");
            Console.WriteLine("│  Type Safety            25%       Proper constraints      │");
            Console.WriteLine("│                                   No unsafe casts         │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Collection Choice      20%       Appropriate use of      │");
            Console.WriteLine("│                                   List/Dictionary/etc.    │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Error Handling         15%       Validation              │");
            Console.WriteLine("│                                   Meaningful exceptions    │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Design Patterns        15%       Factory, Strategy       │");
            Console.WriteLine("│                                   Repository patterns      │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Performance            10%       Time/space complexity   │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Code Clarity           10%       Readable, documented    │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Testing                5%        Edge cases covered      │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");

            Console.WriteLine("\n┌─ GENERICS CONCEPTS TESTED ────────────────────────────────┐");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  ✓ Generic Constraints                                     │");
            Console.WriteLine("│    • where T : class                                       │");
            Console.WriteLine("│    • where T : struct                                      │");
            Console.WriteLine("│    • where T : new()                                       │");
            Console.WriteLine("│    • where T : BaseClass                                   │");
            Console.WriteLine("│    • where T : Interface                                   │");
            Console.WriteLine("│    • where T : U (naked constraint)                        │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  ✓ Collections                                             │");
            Console.WriteLine("│    • List<T> - Ordered, indexed                            │");
            Console.WriteLine("│    • Dictionary<TKey, TValue> - Key-value pairs            │");
            Console.WriteLine("│    • HashSet<T> - Unique items                             │");
            Console.WriteLine("│    • Queue<T>/Stack<T> - FIFO/LIFO                         │");
            Console.WriteLine("│    • SortedDictionary - Ordered                            │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  ✓ Design Patterns                                         │");
            Console.WriteLine("│    • Repository<T>                                         │");
            Console.WriteLine("│    • Factory<T>                                            │");
            Console.WriteLine("│    • Strategy<T>                                           │");
            Console.WriteLine("│    • Wrapper<T>                                            │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  ✓ Performance Considerations                              │");
            Console.WriteLine("│    • Dictionary vs List for lookups                        │");
            Console.WriteLine("│    • IEnumerable vs List for memory                        │");
            Console.WriteLine("│    • Capacity pre-allocation                               │");
            Console.WriteLine("│    • Value types vs Reference types                        │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");

            Console.WriteLine("\n┌─ SCENARIO DIFFICULTY LEVELS ──────────────────────────────┐");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Scenario 1: E-Commerce (Junior Level)                     │");
            Console.WriteLine("│  • Basic generics with single constraint                   │");
            Console.WriteLine("│  • Simple repository pattern                               │");
            Console.WriteLine("│  • List and Dictionary usage                               │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Scenario 2: University (Mid-Level)                        │");
            Console.WriteLine("│  • Multiple generic constraints                            │");
            Console.WriteLine("│  • Complex relationships                                   │");
            Console.WriteLine("│  • Nested generic types                                    │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Scenario 3: Hospital (Mid-Level)                          │");
            Console.WriteLine("│  • Priority queue implementation                           │");
            Console.WriteLine("│  • Type-specific validation                                │");
            Console.WriteLine("│  • SortedDictionary usage                                  │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  Scenario 4: Trading (Senior/Architect)                    │");
            Console.WriteLine("│  • Complex generics with patterns                          │");
            Console.WriteLine("│  • Strategy pattern implementation                         │");
            Console.WriteLine("│  • Performance optimization                                │");
            Console.WriteLine("│  • Advanced collection usage                               │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");

            Console.WriteLine("\n┌─ KEY FEATURES IMPLEMENTED ────────────────────────────────┐");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("│  ✓ Type-safe generic implementations                       │");
            Console.WriteLine("│  ✓ Comprehensive error handling                            │");
            Console.WriteLine("│  ✓ Input validation at all levels                          │");
            Console.WriteLine("│  ✓ XML documentation comments                              │");
            Console.WriteLine("│  ✓ LINQ queries for data manipulation                      │");
            Console.WriteLine("│  ✓ Lambda expressions for delegates                        │");
            Console.WriteLine("│  ✓ Proper encapsulation                                    │");
            Console.WriteLine("│  ✓ Immutable return types (IReadOnlyList)                  │");
            Console.WriteLine("│  ✓ Design patterns (Repository, Strategy, Factory)         │");
            Console.WriteLine("│  ✓ Performance-optimized collection choices                │");
            Console.WriteLine("│                                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
        }
    }
}
