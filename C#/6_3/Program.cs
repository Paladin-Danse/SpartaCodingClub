namespace _6_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Remove(2);
            
            foreach(int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
