using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field: SerializeField] public int gold { get; private set; }
    public List<ItemData> inventoryItems { get; private set; } = new List<ItemData>();
    public ItemSlotUI ItemSlotUI;
    public ItemData EquipItem;
    PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        GetItem("¹ú¸ñµµ³¢");
        GetItem("¹ú¸ñµµ³¢");
        GetItem("Æ°Æ°ÇÑ °©¿Ê");
    }

    public void GetItem(string name)
    {
        if(ItemManager.instance.itemDatas.Any(s => s.name == name))
        {
            int idx = Array.FindIndex(ItemManager.instance.itemDatas, s => s.name == name);
            inventoryItems.Add(ItemManager.instance.itemDatas[idx]);
        }
    }
    public void GetItem(ItemData itemData)
    {
        if (ItemManager.instance.itemDatas.Any(s => s == itemData))
        {
            int idx = Array.FindIndex(ItemManager.instance.itemDatas, s => s == itemData);
            inventoryItems.Add(ItemManager.instance.itemDatas[idx]);
        }
    }
    public void Equip(ItemData itemdata)
    {
        UnEquip();
        EquipItem = itemdata;

        foreach(ItemDataBuffStat buff in EquipItem.buff)
        {
            switch (buff.buffStat)
            {
                case ITEM_BUFFTYPE.Attack:
                    status.buffAttackPoint += buff.buffValue;
                    break;
                case ITEM_BUFFTYPE.Defence:
                    status.buffDefencePoint += buff.buffValue;
                    break;
                case ITEM_BUFFTYPE.Health:
                    status.buffHealthPoint += buff.buffValue;
                    break;
                case ITEM_BUFFTYPE.Critical:
                    status.buffCriticalPoint += buff.buffValue;
                    break;
                default:
                    break;
            }
        }
    }
    public void UnEquip()
    {
        if (EquipItem)
        {
            foreach (ItemDataBuffStat buff in EquipItem.buff)
            {
                switch (buff.buffStat)
                {
                    case ITEM_BUFFTYPE.Attack:
                        status.buffAttackPoint -= buff.buffValue;
                        break;
                    case ITEM_BUFFTYPE.Defence:
                        status.buffDefencePoint -= buff.buffValue;
                        break;
                    case ITEM_BUFFTYPE.Health:
                        status.buffHealthPoint -= buff.buffValue;
                        break;
                    case ITEM_BUFFTYPE.Critical:
                        status.buffCriticalPoint -= buff.buffValue;
                        break;
                    default:
                        break;
                }
            }
            EquipItem = null;
        }
    }
}
