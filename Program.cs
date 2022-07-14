using static System.Console;
using System.Xml.Serialization;
using West.Shape;

namespace West.Shape
{
    // use XmlInclude for derived classes
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Shape
    {
        public string Colour { get; set; } = string.Empty;
        public abstract double Area { get; }
    }
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public override double Area
        {
            get
            {
                return Math.PI * Radius * Radius;
            }
        }
        public Circle() { }
    }
    public class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public override double Area
        {
            get
            {
                return Height * Width;
            }
        }
        public Rectangle() { }
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        var listOfShapes = new List<Shape>
        {
            new Circle { Colour = "Pink", Radius = 2.5d },
            new Rectangle { Colour = "Red", Height = 20.0d, Width = 10.0d },
            new Circle { Colour = "Blue", Radius = 8.0d },
            new Circle { Colour = "Sky", Radius = 12.3d },
            new Rectangle { Colour = "Brown", Height = 45.2d, Width = 18.7d }
        };

        // let's serialize it!
        var path = Path.Combine(Environment.CurrentDirectory, "shapes.xml");
        var xml = new XmlSerializer(typeof(List<Shape>));

        using (StreamWriter fileStream = File.CreateText(path))
        {
            xml.Serialize(fileStream, listOfShapes);
            WriteLine($"Shapes have been serialized to xml file '{path}");
        }
        // let's deserialize!
        using (StreamReader fileStream = File.OpenText(path))
        {
            List<Shape>? loadedXml = xml.Deserialize(fileStream) as List<Shape>;
            foreach (var item in loadedXml!)
            {
                WriteLine("{0} is {1} and has area of {2:N2}",
                    item.GetType().Name, item.Colour, item.Area);
            }
        }
    }
}