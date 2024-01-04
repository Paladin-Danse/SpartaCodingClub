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
            
            bool isBuyMode = false;
            Item[] array_Item = new Item[8];
            while (true)
            {
                int itemNum = 0, userChoice;
                Console.Clear();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Inventory.Instance.inventoryGold} G\n");

                Console.WriteLine("[아이템 목록]");
                
                foreach (Item item in shopItems)
                {
                    if (itemNum > 7) break;
                    bool isBuyed = Inventory.Instance.getInventoryItem.Find(i => i == item) != null;
                    item.ShopItemInfo(isBuyMode, ++itemNum, isBuyed);
                    array_Item[itemNum] = item;
                }
                Console.WriteLine();

                if (isBuyMode)
                {
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 아이템을 선택 해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > itemNum || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage();
                    }
                    else
                    {
                        if (userChoice == 0 && isBuyMode) isBuyMode = false;
                        else
                        {
                            Item buyingItem = shopItems.Find(item => item == array_Item[userChoice]);
                            if (buyingItem != null)
                            {
                                BuyItem(buyingItem);
                            }
                            else
                            {
                                Console.WriteLine("오류 : 구매할 아이템을 찾지 못했습니다.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("0. 나가기\n");

                    Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > 1 || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage();
                    }
                    else
                    {
                        if (userChoice == 1 && isBuyMode == false) isBuyMode = true;
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        public Shop(string shopName)
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

        public void BuyItem(Item item)
        {
            if(Inventory.Instance.getInventoryItem.Find(i => i == item) != null)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                Console.ReadKey();
            }
            else if (Inventory.Instance.inventoryGold >= item.getPrice)
            {
                Inventory.Instance.inventoryGold -= item.getPrice;
                Inventory.Instance.GetItem(item);
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
                Console.ReadKey();
            }
        }
    }
}
