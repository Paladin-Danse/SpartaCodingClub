﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG
{
    public enum ITEM_EFFECT
    {
        ITEM_EFFECT_NONE,
        ITEM_EFFECT_ATP,
        ITEM_EFFECT_DFP,
        ITEM_EFFECT_HP
    }
    public enum ITEM_TYPE
    {
        ITEM_TYPE_NONE,
        ITEM_TYPE_EQUIP,
        ITEM_TYPE_POTION,
        ITEM_TYPE_USEFUL
    }
    public class Item
    {
        protected string name;
        public string getName { get { return name; } }
        protected ITEM_TYPE eType;
        protected ITEM_EFFECT eItemEffect;
        protected string description;
        protected int itemPrice;
        public int getPrice { get { return itemPrice; } }
        public int sellPrice { get { return (int)(itemPrice * 0.85f); } }
        public Item()
        {
            name = "";
            eType = ITEM_TYPE.ITEM_TYPE_NONE;
            eItemEffect = ITEM_EFFECT.ITEM_EFFECT_NONE;
            description = "";
        }
        public Item(string _name, ITEM_TYPE _type, string _description, int _price)
        {
            name = _name;
            eType = _type;
            description = _description;
            itemPrice = _price;
        }
        public virtual void ItemInfo(bool _isEquipMode, int _itemNum)
        {
            if(!_isEquipMode) Console.WriteLine($"- {name}  | {description}");
        }
        public virtual void ShopItemInfo(bool _isBuyMode, bool _isSellMode, int _itemNum, bool _isBuyed)
        {
            if (!_isBuyMode) Console.WriteLine($"- {name}  | {description}      | {(_isBuyed ? "구매완료" :itemPrice)} G");
            if (!_isSellMode) Console.WriteLine($"- {name}  | {description}     | {sellPrice}");
            else Console.WriteLine($"- {_itemNum} {name}  | {description}      | {(_isBuyed ? "구매완료" : itemPrice)} G");
        }
    }
}
