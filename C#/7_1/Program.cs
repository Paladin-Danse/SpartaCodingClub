namespace _7_1
{
    internal class Program
    {
        static void PrintLine()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("■");
            }
            Console.WriteLine();
        }

        static void PrintLine(int count)
        {
            for(int i= 0; i < count; i++)
            {
                Console.Write("■");
            }
            Console.WriteLine();
        }
        static int Add(int a, int b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            PrintLine();
            PrintLine(20);

            int result = Add(10, 20);
            Console.WriteLine(result);
        }
    }
}
