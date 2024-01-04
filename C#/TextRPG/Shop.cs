using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {
        List<Item> shopItems = new List<Item>();
        
        public void ShopOpen()
        {
            if (shopItems.Count == 0)
            {
                Console.WriteLine("오류 : 현재 상점 정보가 없습니다!!");
                Console.ReadKey();
                return;
            }
            int itemNum = 0, userChoice;
            bool isBuyMode = false;

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Inventory.Instance.goldAccess} G\n");

            Console.WriteLine("[아이템 목록]");
            if (shopItems.Count > 0)
            {
                foreach (Item item in shopItems)
                    item.ShopItemInfo(isBuyMode, ++itemNum);
            }
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
            if(int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > 1 || userChoice < 0))
            {
                Program.ReadErrorMessage();
            }
            else
            {

            }
        }

        public void ShopReady(string shopName)
        {
            Equipments equipments;
            switch(shopName)
            {
                case "EquipmentShop":
                    equipments = new Equipments();
                    foreach (Equipment equipment in equipments.equipmentAll)
                    {
                        if(equipment != null)
                            shopItems.Add(equipment);
                    }
                    break;
                default:
                    Console.WriteLine("상점의 정보가 올바르지 않습니다.");
                    Console.ReadKey();
                    return;
            }
        }
    }
}
