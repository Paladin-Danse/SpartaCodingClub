//연습문제 7 - 1~3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1~3번\n");

    string input;
    int i_value;

    do
    {
        Console.WriteLine("이름을 입력해주세요. (3~10글자)");
        input = Console.ReadLine();

        if (input.Length >= 3 && input.Length <= 10)
        {
            Console.WriteLine("안녕하세요! 제 이름은 " + input + " 입니다.");
            break;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("이름을 확인해주세요.");
        }
    } while (true);
}