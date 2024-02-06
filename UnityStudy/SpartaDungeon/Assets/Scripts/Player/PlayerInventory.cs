using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field: SerializeField] public int gold { get; private set; }
    public List<ItemData> inventoryItems { get; private set; } = new List<ItemData>();
    public ItemData EquipItem;
    PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        GetItem("¹ú¸ñµµ³¢");
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
        EquipItem = itemdata;
        if (itemdata.atk > 0)
        {
            status.buffAttackPoint += itemdata.atk;
        }
        if (itemdata.crit > 0)
        {
            status.buffCriticalPoint += itemdata.crit;
        }
        if (itemdata.def > 0)
        {
            status.buffDefencePoint += itemdata.def;
        }
        if (itemdata.hp > 0)
        {
            status.buffHealthPoint += itemdata.hp;
        }
    }
}
