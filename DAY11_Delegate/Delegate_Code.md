using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization
{
    public delegate string PrintMessage(string message);

    public class PrintingCompany
    {
        public PrintMessage CustomerChoicePrintMessage { get; set; }

        public void Print(string message)
        {
           string messageToPrint = CustomerChoicePrintMessage(message);
           Console.WriteLine(messageToPrint);
        }
    }

}



using Serialization;
using System;
using System.Text.Json;

namespace Serialization
{
    public class Program
    {
        static void Main(string[] args)
        {



            PrintingCompany printingCompany = new PrintingCompany();
            printingCompany.CustomerChoicePrintMessage = new PrintMessage(HappyNewYear);
            printingCompany.Print(" Asad");
            Console.ReadLine();
        }

        private static string HappyNewYear(string name)
        {
            return "Happy New Year" + name;
        }


    }
}



using Serialization;
using System;
using System.Text.Json;

namespace Serialization
{
    public class Program
    {
        // 1 - Define a delegate
        public delegate void MyDelegate(string message);

        // 2 - Methods that match the delegate signature
        static void MethodA(string msg) => Console.WriteLine("A: " + msg);
        static void MethodB(string msg) => Console.WriteLine("B: " + msg);
        static void MethodC(string msg) => Console.WriteLine("C: " + msg);
        
        static void Main(string[] args)
        {
            // 3 - Instantiate the delegate and add methods to its invocation list
            MyDelegate del = MethodA;
            del += MethodB; // Multicast delegate
            del += MethodC; // Multicast delegate

            // 4 - Invoke the delegate
            del("Hello, World!");

            Console.ReadLine();
        }
    }
}
