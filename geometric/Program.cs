using System;
using System.Collections.Generic;

interface IGeometric
{
    void move(double moveTo_x, double moveTo_y);
    void rotate(double angle);
}

class Point : IGeometric
{
    public double x { get; set; }
    public double y { get; set; }


public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void move(double moveTo_x, double moveTo_y)
    {
        x = moveTo_x;
        y = moveTo_y;
    }

    public void rotate(double angle)
    {
        double current_x = x;
        double current_y = y;

        // Convert angle to radians
        double radians = angle * Math.PI / 180.0;

        x = current_x * Math.Cos(-radians) - current_y * Math.Sin(-radians);
        y = current_x * Math.Sin(-radians) + current_y * Math.Cos(-radians);

    }

    public override string ToString()
    {
        string formattedX = x.ToString("F2");
        string formattedY = y.ToString("F2");
        return $"Point : x = {formattedX}, y = {formattedY}";
    }


}

class Line : IGeometric
{
    public Point startPoint { get; set; }
    public Point endPoint { get; set; }


public Line(Point startPoint, Point endPoint)
    {
        this.startPoint = new Point(startPoint.x, startPoint.y);
        this.endPoint = new Point(endPoint.x, endPoint.y);
    }

    public void move(double moveTo_x, double moveTo_y)
    {
        double currentMidpoint_x = (startPoint.x + endPoint.x) / 2;
        double currentMidpoint_y = (startPoint.y + endPoint.y) / 2;

        //translation method
        double translate_x = moveTo_x - currentMidpoint_x;
        double translate_y = moveTo_y - currentMidpoint_y;

        startPoint.move(startPoint.x + translate_x, startPoint.y + translate_y);
        endPoint.move(endPoint.x + translate_x, endPoint.y + translate_y);
    }

    public void rotate(double angle)
    {
        startPoint.rotate(angle);
        endPoint.rotate(angle);
    }

    public override string ToString()
    {
        string formattedStartPoint = $"({startPoint.x:F2}, {startPoint.y:F2})";
        string formattedEndPoint = $"({endPoint.x:F2}, {endPoint.y:F2})";
        return $"Line : StartPoint = {formattedStartPoint}, endPoint = {formattedEndPoint}";
    }


}

class Circle : IGeometric
{
    public Point center { get; set; }
    public double radius { get; set; }


public Circle(Point center, double radius)
    {
        this.center = new Point(center.x, center.y);
        this.radius = radius;
    }

    public void move(double moveTo_x, double moveTo_y)
    {
        center.x = moveTo_x;
        center.y = moveTo_y;
    }

    public void rotate(double angle)
    {
        center.rotate(angle);
    }

    public override string ToString()
    {
        string formattedCenter = $"({center.x:F2}, {center.y:F2})";
        string formattedRadius = radius.ToString("F2");
        return $"Circle : Center = {formattedCenter}, radius = {formattedRadius}";
    }



}

class Aggregation : IGeometric
{
    private List<IGeometric> figures = new List<IGeometric>();


public void AddFigure(IGeometric figure)
    {
        figures.Add(figure);
    }

    public void move(double moveTo_x, double moveTo_y)
    {
        foreach (var figure in figures)
        {
            figure.move(moveTo_x, moveTo_y);
        }
    }

    public void rotate(double angle)
    {
        foreach (var figure in figures)
        {
            figure.rotate(angle);
        }
    }

    public override string ToString()
    {
        string result = "";
        foreach (var figure in figures)
        {
            result += figure.ToString() + "\\n";
        }
        return result;
    }



}

class Program
{
    static void Main(string[] args)
    {
        Point point1 = new Point(1, 1);
        Point point2 = new Point(3, 3);
        Line line = new Line(point1, point2);
        Circle circle = new Circle(new Point(2, 2), 2);


    Aggregation aggregation = new Aggregation();
        aggregation.AddFigure(point1);
        aggregation.AddFigure(line);
        aggregation.AddFigure(circle);

        Console.WriteLine("Point Starting Exmample :");
        Console.WriteLine("------------------------------------");
        Console.WriteLine(point2.ToString());
        point2.move(5, 5);
        Console.WriteLine("\\n");
        Console.WriteLine("Point moved :");
        Console.WriteLine(point2.ToString());
        Console.WriteLine("\\n");
        point2.rotate(270);
        Console.WriteLine("Point rotated :");
        Console.WriteLine(point2.ToString());

        lineBreak();

        Console.WriteLine("Line Starting Exmample :");
        lineTitle();
        Console.WriteLine(line.ToString());
        line.move(4, 4);
        Console.WriteLine("\\n");
        Console.WriteLine("Line moved :");
        Console.WriteLine(line.ToString());
        Console.WriteLine("\\n");
        line.rotate(180);
        Console.WriteLine("Line rotated :");
        Console.WriteLine(line.ToString());

        lineBreak();

        Console.WriteLine("Circle Starting Exmample :");
        lineTitle();
        Console.WriteLine(circle.ToString());
        circle.move(5, 5);
        Console.WriteLine("\\n");
        Console.WriteLine("Circle moved :");
        Console.WriteLine(circle.ToString());
        Console.WriteLine("\\n");
        circle.rotate(45);
        Console.WriteLine("Circle rotated :");
        Console.WriteLine(circle.ToString());

        lineBreak();

        Console.WriteLine("Aggregation Starting Example:");
        lineTitle();
        Console.WriteLine(aggregation.ToString());
        aggregation.move(7, 3);
        Console.WriteLine("\\n");
        Console.WriteLine("Aggregation moved :");
        Console.WriteLine(aggregation.ToString());
        Console.WriteLine("\\n");
        aggregation.rotate(180);
        Console.WriteLine("Aggregation rotated :");
        Console.WriteLine(aggregation.ToString());
        lineBreak();
    }

    public static void lineBreak()
    {
        Console.WriteLine("\n\n");
    }

    public static void lineTitle()
    {
        Console.WriteLine("------------------------------------");
    }



}