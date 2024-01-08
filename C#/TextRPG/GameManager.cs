namespace TextRPG
{
    public enum CLASS
    {
        CLASS_WARRIOR,
        CLASS_RANGER,
        CLASS_WIZARD,
        CLASS_CRUSADER
    }
    
    internal class GameManager
    {
        static bool isGameEnd = false;
        static Player player;
        static Shop equipmentShop;
        static Dungeon[] dungeons;
        static Equipments equipments = new Equipments();
        static void InitScene()
        {
            int userChoice;
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("0. 게임 나가기\n");
            
            Console.Write("원하시는 행동을 입력해주세요.\n>>");
            if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > 4 || userChoice < 0))
            {
                ReadErrorMessage("잘못된 입력입니다.");
            }
            else
            {
                switch (userChoice)
                {
                    case 1:
                        StatusCheck();
                        break;
                    case 2:
                        Inventory.Instance.InventoryOpen();
                        break;
                    case 3:
                        equipmentShop.ShopOpen();
                        break;
                    case 4:
                        Enter_theDungeon();
                        break;
                    default:
                        isGameEnd = true;
                        break;
                }
            }
        }
        static void StatusCheck()
        {
            while (true)
            {
                int userChoice;

                Console.Clear();
                player.PlayerInfo();

                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>>");
                if (int.TryParse(Console.ReadLine(), out userChoice) == false || userChoice != 0)
                {
                    ReadErrorMessage("잘못된 입력입니다.");
                }
                else
                {
                    break;
                }
            }
        }
        static void Enter_theDungeon()
        {
            while(true)
            {
                int userChoice;

                Console.Clear();
                for(int i = 1; i < dungeons.Length; i++)
                {
                    Console.WriteLine($"{i}. {dungeons[i].getName}     | 방어력 {dungeons[i].getLeastDFP} 이상 권장");
                }
                Console.WriteLine("0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.>>\n");
                if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > dungeons.Length - 1 || userChoice < 0))
                {
                    ReadErrorMessage("잘못된 입력입니다.");
                }
                else
                {
                    if (userChoice == 0) break;
                    
                    if (player.getHP > 0)
                    {
                        dungeons[userChoice].ExploreDungeon(player);
                    }
                    else
                    {
                        ReadErrorMessage("체력이 모자릅니다. 휴식 먼저 취해주세요.");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string PlayerName;
            while (true)
            {
                Console.Clear();
                Console.Write("시작하기 전 당신의 이름을 알려주세요. : ");
                PlayerName = Console.ReadLine();
                if(PlayerName == null)
                {
                    ReadErrorMessage("잘못된 입력입니다.");
                }
                else break;
            }

            player = new Player(1, PlayerName, CLASS.CLASS_WARRIOR, 10, 5, 100);
            Inventory.Instance.inventoryGold = 5000;
            equipmentShop = new Shop("EquipmentShop");
            
            dungeons = new Dungeon[4];
            dungeons[1] = new Dungeon("쉬운 던전", 5, 1000);
            dungeons[2] = new Dungeon("일반 던전", 11, 1700);
            dungeons[3] = new Dungeon("어려운 던전", 17, 2500);

            //만약 인벤토리 내에 인스턴스가 된 아이템을 만들고 싶을 경우 아래처럼 new Item()형식으로 넣어주면 된다.
            /*
            Inventory.Instance.GetItem(new Equipment(equipments.ironArmor));
            Inventory.Instance.GetItem(new Equipment(equipments.spartanSpear));
            Inventory.Instance.GetItem(new Equipment(equipments.oldSword));
            */
            while (true)
            {
                InitScene();
                if (isGameEnd == true) break;
            }
        }
        static public void ReadErrorMessage(string _Message)
        {
            Console.WriteLine(_Message);
            Console.ReadKey();
        }
        static public void PlayerGetBuffed(ITEM_EFFECT _eItemEffect, int buffPoint)
        {
            switch (_eItemEffect)
            {
                case ITEM_EFFECT.ITEM_EFFECT_ATP:
                    player.buffAttackPoint += buffPoint;
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_DFP:
                    player.buffDefencePoint += buffPoint;
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_HP:
                    player.buffMaxHealth += buffPoint;
                    break;
                default:
                    break;
            }
        }
    }
}
