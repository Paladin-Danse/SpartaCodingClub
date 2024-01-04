// 연습문제 2 - 1번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1번\n");

    int ten = 10;
    int Value_1, Value_2, Value_3, Value_6;
    float Value_4, Value_5;
    Value_1 = ten + 7;
    Value_2 = ten - 3;
    Value_3 = ten * 2;
    Value_4 = (float)ten * 1.5f;
    Value_5 = ten / 3;
    Value_6 = ten % 4;

    Console.WriteLine(
        "Value1 = " + Value_1 + "\n" +
        "Value2 = " + Value_2 + "\n" +
        "Value3 = " + Value_3 + "\n" +
        "Value4 = " + Value_4 + "\n" +
        "Value5 = " + Value_5 + "\n" +
        "Value6 = " + Value_6
        );
}
// 연습문제 2 - 2번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 2번\n");

    string name = "Silver";
    int year = 2023;
    string introduce;
    string thisYear;

    introduce = "안녕하세요. 제 이름은 \"" + name + "\" 입니다.";
    thisYear = "올해는 \'" + year + "년\' 입니다.";
    Console.WriteLine("introduce = " + introduce);
    Console.WriteLine("thisYear = " + thisYear);
}
// 연습문제 2 - 3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 3번\n");

    int ten = 10;

    bool result_1 = ten == 10;
    bool result_2 = ten != 11;
    bool result_3 = ten < 20;
    bool result_4 = ten > 5;

    Console.WriteLine(
        "result_1 = " + result_1 + "\n" +
        "result_2 = " + result_2 + "\n" +
        "result_3 = " + result_3 + "\n" +
        "result_4 = " + result_4
        );
}