using System.Xml.Linq;

namespace ConsoleApp_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int itemCount = 5;
            //string itemName = "HP 포션";
            
            //if (itemCount > 0)
            //{
            //    Console.WriteLine($"보유한 {itemName}의 수량: {itemCount}");
            //}
            //else
            //{
            //    Console.WriteLine($"보유한 {itemName}이 없습니다.");
            //}

            //int score = 100;
            //string playerRank = "";

            //if(score >= 90)
            //{
            //    playerRank = "Diamond";
            //}
            //else if(score >= 80)
            //{
            //    playerRank = "Platinum";
            //}

            //Console.WriteLine("플레이어의 등급은 {0}입니다.", playerRank);

            Console.WriteLine("게임을 시작합니다.");
            Console.WriteLine("1: 전사 / 2: 마법사 / 3: 궁수");
            Console.Write("직업을 선택하세요: ");
            string job = Console.ReadLine();

            switch (job)
            {
                case "1":
                    Console.WriteLine("전사를 선택하셨습니다.");
                    break;
                case "2":
                    Console.WriteLine("마법사를 선택하셨습니다.");
                    break;
                case "3":
                    Console.WriteLine("궁수를 선택하셨습니다.");
                    break;
                default:
                    Console.WriteLine("올바른 값을 입력해주세요.");
                    break;
            }

            Console.WriteLine("게임을 종료합니다.");
        }
    }
}