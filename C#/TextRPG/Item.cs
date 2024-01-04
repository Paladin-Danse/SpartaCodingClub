using System;
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
        public virtual void ShopItemInfo(bool _isBuyMode, int _itemNum)
        {
            if (!_isBuyMode) Console.WriteLine($"- {name}  | {description}      | {itemPrice} G");
        }
    }
}
