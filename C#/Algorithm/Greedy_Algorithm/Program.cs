namespace Greedy_Algorithm
{
    internal class Program
    {
        // 문제: 주어진 동전들로 특정 금액을 만드는데 필요한 최소 동전 수를 구하는 함수를 작성하세요.
        static public int MinCoins(int[] coins, int amount)
        {
            Array.Sort(coins);
            int count = 0;

            for (int i = coins.Length - 1; i >= 0; i--)
            {
                while (amount >= coins[i])
                {
                    amount -= coins[i];
                    count++;
                }
            }

            if (amount > 0) return -1;

            return count;
        }

        static void Main(string[] args)
        {
            int[] coins = { 10, 50, 100, 500};
            int result = MinCoins(coins, 760);
            if (result != -1) Console.WriteLine($"필요한 최소 동전의 수 : {result}");
            else Console.WriteLine("지금 가지고 있는 동전으로는 구할 수 없는 정보입니다.");
        }
    }
}
