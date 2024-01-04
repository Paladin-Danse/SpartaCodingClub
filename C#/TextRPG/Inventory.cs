using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Inventory
    {
        private static Inventory inventory;
        public static Inventory Instance
        {
            get
            {
                if (inventory == null) inventory = new Inventory();
                return inventory;
            }
        }
        //인벤토리 안의 아이템
        List<Item> items = new List<Item>();
        public List<Item> getInventoryItem { get { return items; } }
        //장착관리 On/Off
        bool isEquipMode = false;
        //골드
        int gold;
        public int inventoryGold { get { return gold; } set { gold = value; } }
        public void InventoryOpen()
        {
            while (true)
            {
                int userChoice;
                Equipment[] array_equipment = new Equipment[8];//한페이지당 7개가 한계. 8=이전,9=다음,0=나가기
                int itemNum = 0;
                Console.Clear();
                Console.WriteLine("[아이템 목록]");

                if (items.Count > 0)
                {
                    //장착관리 온
                    if (isEquipMode)
                    {
                        foreach (Equipment item in items)
                        {
                            //표시된 아이템이 7개를 넘어가면 입력할 숫자의 개수가 부족하므로 탈출.
                            //이후 다음페이지를 만들든 후속조치 단계가 올 때까지는 보류.
                            if (itemNum > 7) break;
                            item.ItemInfo(isEquipMode, ++itemNum);
                            array_equipment[itemNum] = item;
                        }
                    }
                    //장착관리 오프
                    //장착을 하지않는 일반 아이템(포션, 열쇠, etc...)도 표시하기 위한 코드.
                    else
                    {
                        foreach (Item item in items)
                        {
                            item.ItemInfo(isEquipMode, ++itemNum);
                        }
                    }
                }

                Console.WriteLine();

                if (isEquipMode == false)
                {
                    Console.WriteLine("1. 장착관리");
                    Console.WriteLine("0. 나가기\n");

                    Console.Write("원하시는 행동을 입력해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > 1 || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage();
                    }
                    else
                    {
                        if (userChoice == 1 && isEquipMode == false)
                        {
                            isEquipMode = true;
                        }
                        else break;
                    }
                }
                else
                {
                    Console.WriteLine("0. 나가기");
                    Console.Write("장착하실 장비를 입력해주세요.\n>>");
                    if (int.TryParse(Console.ReadLine(), out userChoice) == false || (userChoice > itemNum || userChoice < 0))
                    {
                        GameManager.ReadErrorMessage();
                    }
                    else
                    {
                        if (userChoice == 0 && isEquipMode) isEquipMode = false;
                        Equipment equipment = null;
                        if (items.Find(item => item == array_equipment[userChoice]) != null)
                            equipment = (Equipment)items.Find(item => item == array_equipment[userChoice]);
                        
                        if (equipment != null)
                        {
                            equipment.onEquip = !equipment.onEquip;
                            if (equipment.onEquip)
                                GameManager.PlayerGetBuffed(equipment.getItemEffect, equipment.getBuffPoint);
                            else
                                GameManager.PlayerGetBuffed(equipment.getItemEffect, -equipment.getBuffPoint);
                        }
                        else
                        {
                            Console.WriteLine("오류 : 장착하실 장비의 데이터를 찾지 못했습니다.");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
        public void GetItem(Item _item)
        {
            items.Add(_item);
        }
    }
}
