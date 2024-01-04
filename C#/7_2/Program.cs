namespace _7_2
{
    // 1) 오버로딩
    internal class Program
    {
        //    static int AddNumbers(int a, int b)
        //    {
        //        return a + b;
        //    }
        //    static float AddNumbers(float a, float b)
        //    {
        //        return a + b;
        //    }
        //    static int AddNumbers(int a, int b, int c)
        //    {
        //        return a + b + c;
        //    }
        // 2) 재귀함수
        static void CountDown(int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine(n);
                CountDown(n - 1);  // 자기 자신을 호출
            }
        }
        static void Main(string[] args)
        {
            CountDown(5);
        }
    }
}
