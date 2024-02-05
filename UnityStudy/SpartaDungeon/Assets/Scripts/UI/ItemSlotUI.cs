using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public ItemData itemData { get; private set; }
    Image itemSprite;

    public void setItem(ItemData itemData)
    {
        if(itemSprite == null) itemSprite = transform.Find("Back/ItemSprite").GetComponent<Image>();
        if (itemData)
        {
            itemSprite.enabled = true;
            itemSprite.sprite = itemData.sprite;
            this.itemData = itemData;
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.enabled = false;
            this.itemData = null;
        }
    }
}
