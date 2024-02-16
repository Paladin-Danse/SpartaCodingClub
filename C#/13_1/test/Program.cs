namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            int[] i = solution.solution(3, score: [0, 300, 40, 300, 20, 70, 150, 50, 500, 1000]);

            for(int j=0; j<i.Length; j++)
                Console.Write(i[j] + " ");
        }
    }
    public class Solution
    {
        public int[] solution(int k, int[] score)
        {
            int[] answer = new int[score.Length];
            int[] HallofFame = new int[k];
            for (int i = 0; i < score.Length; i++)
            {
                if (i >= k)
                {
                    int minScore = HallofFame.Min();
                    if (minScore < score[i])
                    {
                        HallofFame[Array.IndexOf(HallofFame, minScore)] = score[i];
                        minScore = HallofFame.Min();
                    }
                    answer[i] = minScore;
                }
                else
                {
                    HallofFame[i] = score[i];
                    answer[i] = HallofFame.Where(x => x != 0).Min();
                }
            }

            for (int i = 0; i < HallofFame.Length; i++)
                Console.WriteLine(HallofFame[i]);
            return answer;
        }
    }
}
