using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    ItemData itemData;
    Image itemImage;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    Transform itemEffectDescription;
    List<BuffStatUI> buffStatList = new List<BuffStatUI>();
    [SerializeField] private GameObject buffedStat;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryUI inventoryUI;

    public void WindowOpen(ItemData itemdata)
    {
        if (!itemImage) itemImage = transform.Find("ItemSprite_BG/Image").GetComponent<Image>();
        if (!itemName) itemName = transform.Find("ItemInfo/ItemName").GetComponent<TextMeshProUGUI>();
        if (!itemDescription) itemDescription = transform.Find("ItemInfo/Description").GetComponent<TextMeshProUGUI>();
        if (!itemEffectDescription) itemEffectDescription = transform.Find("ItemEffectDescription");

        itemData = itemdata;
        itemImage.sprite = itemdata.sprite;
        itemName.text = itemdata.name;
        itemDescription.text = itemdata.description;

        if(itemdata.buff.Length > 0)
        {
            BuffStatUI buffStatUI;

            foreach (BuffStatUI item in buffStatList)
                item.gameObject.SetActive(false);

            foreach (ItemDataBuffStat stat in itemdata.buff)
            {
                BuffStatUI tempUI = buffStatList.Find(i => i.gameObject.activeSelf == false);
                if (tempUI == null)
                    buffStatUI = Instantiate(buffedStat, itemEffectDescription).GetComponent<BuffStatUI>();
                else buffStatUI = tempUI;

                buffStatUI.SetUI(stat.buffStat, stat.buffValue);
                buffStatUI.gameObject.SetActive(true);

                buffStatList.Add(buffStatUI);
            }
        }
    }

    public void EquipItem()
    {
        if(itemData)
        {
            playerInventory.Equip(itemData);
            inventoryUI.EquipItem(itemData);
        }
        gameObject.SetActive(false);
    }

    public void WindowClose()
    {
        gameObject.SetActive(false);
    }
}
