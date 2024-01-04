// 연습문제 3 - 1~2번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1~2번\n");

    Console.WriteLine("데이터를 입력해주세요.");
    string input = Console.ReadLine();
    int i_value; bool b_value;
    if (int.TryParse(input, out i_value))
        Console.WriteLine("숫자입니다.");
    else if (bool.TryParse(input, out b_value))
        Console.WriteLine("불리언입니다.");
    else
        Console.WriteLine("문자열입니다.");
}
// 연습문제 3 - 3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 3번\n");

    string input;
    int i_value;

    Console.WriteLine("데이터를 입력해주세요.");
    input = Console.ReadLine();

    if (int.TryParse(input, out i_value))
    {
        if (i_value >= 100)
            Console.WriteLine(i_value + "은(는) 100보다 같거나 큰 수 입니다.");
        else
            Console.WriteLine(i_value + "은(는) 100보다 작은 수 입니다.");
    }
    else
        Console.WriteLine("숫자가 아닙니다.");
}
// 연습문제 3 - 4번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 4번\n");

    string input;
    int i_value;

    Console.WriteLine("데이터를 입력해주세요.");
    input = Console.ReadLine();

    if(int.TryParse(input, out i_value))
    {
        if (i_value % 2 == 0)
            Console.WriteLine(i_value + "은(는) 짝수입니다.");
        else
            Console.WriteLine(i_value + "은(는) 홀수입니다.");
    }
    else
        Console.WriteLine("숫자가 아닙니다.");
}