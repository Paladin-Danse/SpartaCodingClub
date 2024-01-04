
// 연습문제 1 - 1~2번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 1~2번\n");

    int level, count;
    float percent, speed;
    string nickname, description;

    level = 1;
    count = 5;
    percent = 50.0f;
    speed = 20.0f;
    nickname = "Silver";
    description = "몰?루";

    Console.WriteLine(
        "level = " + level + "\n" +
        "count = " + count + "\n" +
        "percent = " + percent + "\n" +
        "speed = " + speed + "\n" +
        "nickname = " + nickname + "\n" +
        "description = " + description
        );
}

// 연습문제 1 - 3번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 3번\n");

    int iTen = 10;
    float fTen = (float)iTen;

    float fFive = 5.5f;
    int iFive = (int)fFive;

    Console.WriteLine("fTen = " + fTen.ToString());
    Console.WriteLine("iFive = " + iFive.ToString());
}

// 연습문제 1 - 4번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 4번\n");

    int n = 10;
    float f = 0.5f;

    Console.WriteLine("n = " + n.ToString());
    Console.WriteLine("f = " + f.ToString());
}

// 연습문제 1 - 5번
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("연습문제 5번\n");

    string strTen = "10";
    string strSix = "6.2";

    int iTen;
    float fSix;

    if (int.TryParse(strTen, out iTen))
        Console.WriteLine("iTen = " + int.Parse(strTen));

    if (float.TryParse(strSix, out fSix))
        Console.WriteLine("fSix = " + float.Parse(strSix));
}