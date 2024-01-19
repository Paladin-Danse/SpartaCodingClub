namespace Dynamic_Programming
{
    internal class Program
    {
        static public int Fibonacci(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 0;
            dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }
        static void Main(string[] args)
        {
            int n = 10;
            for(int i = 1; i <= n; i++)
                Console.Write(Fibonacci(i) + " ");
        }
    }
}
