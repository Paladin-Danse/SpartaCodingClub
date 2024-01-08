using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Dungeon
    {
        string name;
        public string getName { get { return name; } }
        int leastDFP;
        public int getLeastDFP { get { return leastDFP; } }
        int minDamage;
        int maxDamage;
        int defaultReward;
        public int getReward { get { return defaultReward; } }
        public Dungeon(string _name, int _needDefence, int _defaultReward)
        {
            name = _name;
            leastDFP = _needDefence;
            minDamage = 20;
            maxDamage = 35;
            defaultReward = _defaultReward;
        }

        public Player ExploreDungeon(Player player)
        {
            int playerDamage, rewardResult;

            if (player.currentDefencePoint < leastDFP)
            {
                Random random = new Random();
                if (random.Next(0, 10) >= 6)
                {
                    playerDamage = player.currentMaxHealth / 2;
                    rewardResult = 0;

                    Console.WriteLine($"{name}을 실패하셨습니다.\n");

                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {player.getHP} -> {Math.Clamp(player.getHP - playerDamage, 0, player.currentMaxHealth)}");
                    Console.WriteLine($"Gold {Inventory.Instance.inventoryGold} G -> {Inventory.Instance.inventoryGold + rewardResult} G\n");
                    player.Damaged(playerDamage);

                    Console.WriteLine("아무 키나 눌러 진행해주세요.");
                    Console.ReadKey();
                    return player;
                }
            }

            playerDamage = DungeonDamage(player.currentDefencePoint);
            rewardResult = RewardCalculation(player.currentAttackPoint);

            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{name}을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {player.getHP} -> {Math.Clamp(player.getHP - playerDamage, 0, player.currentMaxHealth)}");
            Console.WriteLine($"Gold {Inventory.Instance.inventoryGold} G -> {Inventory.Instance.inventoryGold + rewardResult} G\n");
            Inventory.Instance.inventoryGold += rewardResult;
            player.Damaged(playerDamage);

            Console.WriteLine("아무 키나 눌러 진행해주세요.");
            Console.ReadKey();
            return player;
        }

        public int DungeonDamage(int _DFP)
        {
            Random random = new Random();

            int minRange = minDamage + (leastDFP - _DFP);
            minRange = minRange < 0 ? 0 : minRange;
            int maxRange = maxDamage + (leastDFP - _DFP);
            maxRange = maxRange < 0 ? 0 : maxRange;

            //Next함수에서 최대값 maxRange는 램덤값에서 포함이 안된다. 그러므로 +1 시켜 maxRange도 포함시켜야 함.
            return random.Next(minRange, maxRange + 1);
        }
        public int RewardCalculation(int _ATP)
        {
            Random random = new Random();
            return defaultReward +
                random.Next
                ((int)(defaultReward * (0.01f * _ATP)),//추가보상 최소값
                (int)(defaultReward * (0.02f * _ATP)));//추가보상 최대값
        }
    }
}
