namespace _9_2_2
{
    abstract class Shape
    {
        public abstract void Draw();
    }

    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Circle");
        }
    }

    class Square : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Square");
        }
    }

    class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Triangle");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Shape> list = new List<Shape>();
            list.Add(new Circle());
            list.Add(new Square());
            list.Add(new Triangle());

            foreach(Shape shape in list)
            {
                shape.Draw();
            }
        }
    }
}
