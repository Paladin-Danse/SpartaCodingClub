using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public ItemSlotUI[] inventoryItem;

    public void OnPanel(bool setBool)
    {
        if(setBool)
        {
            int cnt = 0;
            foreach(ItemData data in inventory.inventoryItems)
            {
                inventoryItem[cnt++].setItem(data);
            }
            for(; cnt < inventoryItem.Length; cnt++)
            {
                inventoryItem[cnt].setItem(null);
            }
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
}
