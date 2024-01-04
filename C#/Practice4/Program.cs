// 연습문제 4 - 1번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1번\n");

    string input1, input2;
    int i_value1, i_value2;

    Console.WriteLine("첫번째 데이터를 입력해주세요.");
    input1 = Console.ReadLine();
    Console.WriteLine("두번째 데이터를 입력해주세요.");
    input2 = Console.ReadLine();

    if (int.TryParse(input1, out i_value1) && int.TryParse(input2, out i_value2))
        Console.WriteLine("두 데이터는 모두 숫자입니다.");
    else
        Console.WriteLine("숫자가 아닙니다.");
}
// 연습문제 4 - 2번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 2번\n");

    string input1, input2;
    int i_value1, i_value2;
    
    Console.WriteLine("첫번째 데이터를 입력해주세요.");
    input1 = Console.ReadLine();
    Console.WriteLine("두번째 데이터를 입력해주세요.");
    input2 = Console.ReadLine();

    if (int.TryParse(input1, out i_value1) || int.TryParse(input2, out i_value2))
    {
        if(int.TryParse(input1, out i_value1) && int.TryParse(input2, out i_value2))
            Console.WriteLine("두 데이터는 모두 숫자입니다.");
        else
            Console.WriteLine("하나의 데이터만 숫자입니다.");
    }
    else
        Console.WriteLine("두 데이터 모두 숫자가 아닙니다.");
}
// 연습문제 4 - 3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 3번\n");

    string input1, input2;
    int i_value1, i_value2;

    Console.WriteLine("첫번째 데이터를 입력해주세요.");
    input1 = Console.ReadLine();
    Console.WriteLine("두번째 데이터를 입력해주세요.");
    input2 = Console.ReadLine();

    if (int.TryParse(input1, out i_value1) && int.TryParse(input2, out i_value2))
    {
        if (i_value1 == i_value2) Console.WriteLine(i_value1 + "와(과) " + i_value2 + "은(는) 같습니다.");
        else
        {
            if(i_value1 > i_value2) Console.WriteLine(i_value1 + "은(는) " + i_value2 + " 보다 큽니다.");
            else Console.WriteLine(i_value1 + "은(는) " + i_value2 + " 보다 작습니다.");
        }
    }
    else
        Console.WriteLine("두 개의 숫자를 입력해주세요.");
}