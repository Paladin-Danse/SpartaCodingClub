namespace _11_3
{
    internal class Program
    {
        public enum Month
        {
            Jan = 1,
            Feb,
            Mar,
            Apr,
            May,
            Jun,
            Jul,
            Aug,
            Sep,
            Oct,
            Nov,
            Dec
        }
        public static void ProcessMonth(int month)
        {
            if (month >= (int)Month.Jan && month <= (int)Month.Dec)
            {
                Month selectMonth = (Month)month;
                Console.WriteLine("선택한 월은 {0}입니다.", selectMonth);
            }
        }
        static void Main(string[] args)
        {
            int userInput = 12;
            ProcessMonth(userInput);
        }
    }
}
