﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    public enum EQUIPMENT
    {
        EQUIPMENT_NONE = 0,
        EQUIPMENT_WEAPON,
        EQUIPMENT_ARMOR,
        EQUIPMENT_ACCESSORY
    }
    public class Equipment : Item
    {
        bool isEquip;
        public bool onEquip
        {
            get { return isEquip; }
            set 
            {
                if (isEquip != value)
                {
                    isEquip = value;
                    if (isEquip)
                        GameManager.PlayerGetBuffed(getItemEffect, getBuffPoint);
                    else
                        GameManager.PlayerGetBuffed(getItemEffect, -getBuffPoint);
                }
            }
        }
        int buffPoint;
        public int getBuffPoint { get { return buffPoint; } }
        public ITEM_EFFECT getItemEffect { get { return eItemEffect; } }
        EQUIPMENT eEquipMent;
        public EQUIPMENT getEnumEquipment { get { return eEquipMent; } }

        public Equipment(string _name, ITEM_EFFECT _eItemEffect, EQUIPMENT _eEquipMent, int _buffPoint, string _description, int _price)
        {
            name = _name;
            isEquip = false;
            eItemEffect = _eItemEffect;
            eEquipMent = _eEquipMent;
            buffPoint = _buffPoint;
            description = _description;
            itemPrice = _price;
        }
        public Equipment(Equipment _equipment)
        {
            name = _equipment.name;
            isEquip = false;
            eItemEffect = _equipment.eItemEffect;
            buffPoint = _equipment.buffPoint;
            description = _equipment.description;
        }
        public Equipment GetEquipment()
        {
            return this;
        }

        public override void ItemInfo(bool _isEquipMode, int _itemNum)
        {
            string itemEffect;
            switch (eItemEffect)
            {
                case ITEM_EFFECT.ITEM_EFFECT_ATP:
                    itemEffect = "공격력";
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_DFP:
                    itemEffect = "방어력";
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_HP:
                    itemEffect = "체력";
                    break;
                default:
                    itemEffect = "";
                    break;
            }

            Console.WriteLine($"- {(_isEquipMode ? _itemNum.ToString() + " " : "")}{(isEquip ? "[E]" : "")}{name}  | {itemEffect} {(buffPoint >=0 ? "+" : "-")}{buffPoint} | {description}");
        }
        public override void ShopItemInfo(bool _isBuyMode, bool _isSellMode, int _itemNum, bool _isBuyed)
        {
            string itemEffect;
            string price = "", modeNum = "";
            switch (eItemEffect)
            {
                case ITEM_EFFECT.ITEM_EFFECT_ATP:
                    itemEffect = "공격력";
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_DFP:
                    itemEffect = "방어력";
                    break;
                case ITEM_EFFECT.ITEM_EFFECT_HP:
                    itemEffect = "체력";
                    break;
                default:
                    itemEffect = "없음";
                    break;
            }
            if(_isBuyMode)
            {
                price = itemPrice.ToString();
                modeNum = _itemNum.ToString() + " ";
            }
            else if(_isSellMode)
            {
                price = sellPrice.ToString("N0");
                modeNum = _itemNum.ToString() + " ";
            }
            else if(_isBuyMode == false || _isSellMode == false)
            {
                price = itemPrice.ToString();
                modeNum = "";
            }
            else
            {
                GameManager.ReadErrorMessage("오류 : 예외처리 부족!!");
            }
            Console.WriteLine($"- {modeNum}{name}  | {itemEffect} {(buffPoint >= 0 ? "+" : "")}{buffPoint} | {description}      | {(_isBuyed ? "구매완료" : price)}");

        }
    }
    public struct Equipments
    {
        public List<Equipment> equipmentAll = new List<Equipment>();
        public Equipment ironArmor;
        public Equipment trainingArmor;
        public Equipment spartanArmor;
        public Equipment spartanSpear;
        public Equipment oldSword;
        public Equipment bronzeAxe;

        public Equipments()
        {
            equipmentAll.Add(trainingArmor = new Equipment("수련자 갑옷", ITEM_EFFECT.ITEM_EFFECT_DFP, EQUIPMENT.EQUIPMENT_ARMOR, +5, "수련에 도움을 주는 갑옷입니다.", 1000));
            equipmentAll.Add(ironArmor = new Equipment("무쇠갑옷", ITEM_EFFECT.ITEM_EFFECT_DFP, EQUIPMENT.EQUIPMENT_ARMOR, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000));
            equipmentAll.Add(spartanArmor = new Equipment("스파르타의 갑옷", ITEM_EFFECT.ITEM_EFFECT_DFP, EQUIPMENT.EQUIPMENT_ARMOR, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            equipmentAll.Add(oldSword = new Equipment("낡은 검", ITEM_EFFECT.ITEM_EFFECT_ATP, EQUIPMENT.EQUIPMENT_WEAPON, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
            equipmentAll.Add(bronzeAxe = new Equipment("청동 도끼", ITEM_EFFECT.ITEM_EFFECT_ATP, EQUIPMENT.EQUIPMENT_WEAPON, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
            equipmentAll.Add(spartanSpear = new Equipment("스파르타의 창", ITEM_EFFECT.ITEM_EFFECT_ATP, EQUIPMENT.EQUIPMENT_WEAPON, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000));
        }
    }
}