using System;
using System.Globalization;

interface IArea
{
    double GetArea();
}

abstract class Shape : IArea
{
    public abstract double GetArea();
}

class Circle : Shape
{
    private double radius;

    public Circle(double r)
    {
        radius = r;
    }

    public override double GetArea()
    {
        return Math.PI * radius * radius;
    }
}

class Rectangle : Shape
{
    private double width, height;

    public Rectangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public override double GetArea()
    {
        return width * height;
    }
}

class Triangle : Shape
{
    private double baseVal, height;

    public Triangle(double b, double h)
    {
        baseVal = b;
        height = h;
    }

    public override double GetArea()
    {
        return 0.5 * baseVal * height;
    }
}

class Program
{
    static void Main()
    {
        // Sample input
        string[] shapes =
        {
            "C 3",
            "R 4 5",
            "T 6 2"
        };

        double totalArea = ComputeTotalArea(shapes);

        Console.WriteLine(
            Math.Round(totalArea, 2, MidpointRounding.AwayFromZero)
        );
    }

    static double ComputeTotalArea(string[] shapes)
    {
        double totalArea = 0.0;

        foreach (string shapeStr in shapes)
        {
            if (string.IsNullOrWhiteSpace(shapeStr))
                continue;

            string[] parts = shapeStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            char type = parts[0][0];

            Shape shape = null;

            switch (type)
            {
                case 'C':
                    shape = new Circle(double.Parse(parts[1], CultureInfo.InvariantCulture));
                    break;

                case 'R':
                    shape = new Rectangle(
                        double.Parse(parts[1], CultureInfo.InvariantCulture),
                        double.Parse(parts[2], CultureInfo.InvariantCulture));
                    break;

                case 'T':
                    shape = new Triangle(
                        double.Parse(parts[1], CultureInfo.InvariantCulture),
                        double.Parse(parts[2], CultureInfo.InvariantCulture));
                    break;
            }

            if (shape != null)
                totalArea += shape.GetArea();
        }

        return totalArea;
    }
}
