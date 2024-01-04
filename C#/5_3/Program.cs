namespace _5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) 구구단
            //for (int i = 1; i <= 9; i++)
            //{
            //    for (int j = 2; j <= 9; j++)
            //    {
            //        Console.Write("{0} x {1} = {2} \t", j, i, i * j);
            //    }
            //    Console.WriteLine();
            //}

            // 2) break와 continue
            //for (int i = 1; i <= 10; i++)
            //{
            //    if (i % 3 == 0)
            //    {
            //        continue; // 3의 배수인 경우 다음 숫자로 넘어감
            //    }

            //    Console.WriteLine(i);
            //    if (i == 7)
            //    {
            //        break; // 7이 출력된 이후에는 반복문을 빠져나감
            //    }
            //}

            // 2) break와 continue
            int sum = 0;

            while (true)
            {
                Console.Write("숫자를 입력하세요: ");
                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {
                    Console.WriteLine("프로그램을 종료합니다.");
                    break;
                }

                if (input < 0)
                {
                    Console.WriteLine("음수는 무시합니다.");
                    continue;
                }

                sum += input;
                Console.WriteLine("현재까지의 합: " + sum);
            }

            Console.WriteLine("합계: " + sum);
        }
    }
}
