using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    public ItemData itemData { get; private set; }
    Image itemSprite;
    [SerializeField] private PopupWindow popupWindow;
    private Image equipUI;
    public void setItem(ItemData itemdata)
    {
        if(itemSprite == null) itemSprite = transform.Find("Back/ItemSprite").GetComponent<Image>();
        if (equipUI == null) equipUI = transform.Find("Back/Image/Equip").GetComponent<Image>();
        itemData = itemdata;

        if (itemData)
        {
            itemSprite.enabled = true;
            itemSprite.sprite = itemData.sprite;
            if (inventory.EquipItem == itemData) setEquipUI(true);
            else if (inventory.EquipItem != itemData && equipUI.gameObject.activeSelf) setEquipUI(false);
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.enabled = false;
            itemData = null;
        }
    }

    public void PopupOpen()
    {
        if (itemSprite.enabled)
        {
            if (popupWindow.gameObject.activeSelf == false)
                popupWindow.gameObject.SetActive(true);
            popupWindow.WindowOpen(itemData);
        }
    }

    public void setEquipUI(bool setBool)
    {
        equipUI.gameObject.SetActive(setBool);
    }
}
