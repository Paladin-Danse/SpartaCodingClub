//연습문제 6 - 1번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1번\n");

    int i_value = 2;

    for(int i = 2; i <= 9; i++)
    {
        Console.WriteLine(i_value + " x " + i + " = " + i_value * i);
    }
}
//연습문제 6 - 2번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 2번\n");

    string input;
    int i_value;

    Console.WriteLine("출력하고 싶은 단을 입력해주세요.");
    input = Console.ReadLine();

    if (int.TryParse(input, out i_value))
    {
        for(int i = 2; i <= 9; i++)
        {
            Console.WriteLine(i_value + " x " + i + " = " + i_value * i);
        }
    }
    else Console.WriteLine("숫자가 아닙니다.");
}
//연습문제 6 - 3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 3번\n");

    int i_first = 0, i_second = 1;
    int i_fibo;

    Console.Write(i_second + " ");
    for(int i = 1; i < 10; i++)
    {
        i_fibo = i_first + i_second;
        Console.Write(i_fibo + " ");

        i_first = i_second;
        i_second = i_fibo;
    }
    Console.WriteLine();
}
//연습문제 6 - 4번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 4번\n");

    int i_first = 0, i_second = 1;
    int i_fibo;
    string input;
    int i_value;

    Console.WriteLine("몇개의 피보나치 수열을 출력하고 싶으신가요?");
    input = Console.ReadLine();

    if (int.TryParse(input, out i_value))
    {
        if (i_value >= 47)
        {
            Console.WriteLine("숫자가 너무 큽니다.");
        }
        else if (i_value <= 0)
        {
            Console.WriteLine("양수를 입력해주세요.");
        }
        else
        {
            Console.Write(i_second + " ");
            for (int i = 1; i < i_value; i++)
            {
                i_fibo = i_first + i_second;
                Console.Write(i_fibo + " ");

                i_first = i_second;
                i_second = i_fibo;
            }
        }
    }
    else Console.WriteLine("숫자가 아닙니다.");
}
