using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a: ");
        //checking if i/p is integer or not
        if(!int.TryParse(Console.ReadLine(), out int a)) // 'out' is used to return the converted number from TryParse if input is valid.

        {
            Console.WriteLine("Error:InvalidNumber");
            return;
        }
        Console.WriteLine("Enter b: ");
        if(!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Error:InvalidNumber");
            return;
        }

        Console.WriteLine("Enter operand(+,-,/,*): ");
        string op = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(op))
        {
            Console.WriteLine("Error: InvalidExpression");
            return;
        }

        switch (op)
        {
            case "+":
            Console.WriteLine(a+b);
            break;

            case "-":
            Console.WriteLine(a-b);
            break;

            case "/":
            if(b == 0)
                Console.WriteLine("Error:DivideByZero");
            Console.WriteLine(a/b);
            break;

            case "*":
            Console.WriteLine(a*b);
            break;

            default:
            Console.WriteLine("Error:UnknownOperator");
            break;
        }

    }
}