namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            int i = solution.solution("one4seveneight");

            Console.WriteLine(i);
        }
    }
    public class Solution
    {
        public int solution(string s)
        {
            int answer = 0;
            string[] numString = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            while (!int.TryParse(s, out answer))
            {
                for (int i = 0; i < numString.Length; i++)
                {
                    s = s.Replace(numString[i], i.ToString());
                }
            }

            return answer;
        }
    }
}
