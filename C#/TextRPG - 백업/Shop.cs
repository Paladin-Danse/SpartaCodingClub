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
                GameManager.ReadErrorMessage("오류 : 현재 상점 정보가 없습니다!!");
                return;
            }
            
            bool isBuyMode = false;
            bool isSellMode = false;
            Item[] array_Item = new Item[8];
            while (true)
            {
                int itemNum = 0, userChoice;
                Console.Clear();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Inventory.Instance.inventoryGold} G\n");

                Console.WriteLine("[아이템 목록]");

                if (isSellMode == false)
                {
                    foreach (Item item in shopItems)
                    {
                        if (itemNum > 7) break;
                        bool isBuyed = Inventory.Instance.getInventoryItem.Find(i => i == item) != null;
                        item.ShopItemInfo(isBuyMode, isSellMode, ++itemNum, isBuyed);
                        array_Item[itemNum] = item;
                    }
                }
                else
                {
                    foreach(Item item in Inventory.Instance.getInventoryItem)
                    {
                        if(itemNum > 7) break;
                        item.ShopItemInfo(false, isSellMode, ++itemNum, false);
                        array_Item[itemNum] = item;
                    }
                }
                Console.WriteLine();

                //구매모드|판매모드|일반
                if (isBuyMode)
                {
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 아이템을 선택 해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > itemNum || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage("잘못된 입력입니다.");
                    }
                    else
                    {
                        if (userChoice == 0 && isBuyMode)
                        {
                            isBuyMode = false;
                            continue;
                        }
                        else
                        {
                            Item buyingItem = shopItems.Find(item => item == array_Item[userChoice]);
                            if (buyingItem != null)
                            {
                                BuyItem(buyingItem);
                            }
                            else
                            {
                                GameManager.ReadErrorMessage("오류 : 구매할 아이템을 찾지 못했습니다.");
                            }
                        }
                    }
                }
                else if(isSellMode)
                {
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 아이템을 선택 해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > itemNum || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage("잘못된 입력입니다.");
                    }
                    else
                    {
                        if (userChoice == 0 && isSellMode)
                        {
                            isSellMode = false;
                            continue;
                        }
                        else
                        {
                            Item SellingItem = Inventory.Instance.getInventoryItem.Find(item => item == array_Item[userChoice]);
                            if (SellingItem != null)
                            {
                                SellItem(SellingItem);
                            }
                            else
                            {
                                GameManager.ReadErrorMessage("오류 : 구매할 아이템을 찾지 못했습니다.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 나가기\n");

                    Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > 2 || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage("잘못된 입력입니다.");
                    }
                    else
                    {
                        if (userChoice == 1 && isBuyMode == false)
                        {
                            isBuyMode = true;
                            isSellMode = false;
                        }
                        else if (userChoice == 2 && isSellMode == false)
                        {
                            isSellMode = true;
                            isBuyMode = false;
                        }
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
                    GameManager.ReadErrorMessage("상점의 정보가 올바르지 않습니다.");
                    return;
            }
        }

        public void BuyItem(Item item)
        {
            if(Inventory.Instance.getInventoryItem.Find(i => i == item) != null)
            {
                GameManager.ReadErrorMessage("이미 구매한 아이템입니다.");
            }
            else if (Inventory.Instance.inventoryGold >= item.getPrice)
            {
                Inventory.Instance.inventoryGold -= item.getPrice;
                Inventory.Instance.GetItem(item);
                GameManager.ReadErrorMessage("구매를 완료했습니다.");
            }
            else
            {
                GameManager.ReadErrorMessage("Gold 가 부족합니다.");
            }
        }

        public void SellItem(Item item)
        {
            if (Inventory.Instance.getInventoryItem.Find(i => i == item) != null)
            {
                Inventory.Instance.inventoryGold += item.sellPrice;
                Inventory.Instance.LostItem(item);
                GameManager.ReadErrorMessage("판매를 완료했습니다.");
            }
            else
            {
                GameManager.ReadErrorMessage("오류 : 아이템을 찾을 수 없습니다!!");
            }
        }
    }
}
