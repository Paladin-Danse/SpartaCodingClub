using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffStatUI : MonoBehaviour
{
    [SerializeField] ITEM_BUFFTYPE type;
    [SerializeField] int value;

    Image buffImage;
    TextMeshProUGUI buffNameText;
    TextMeshProUGUI buffValueText;

    public void SetUI(ITEM_BUFFTYPE newType, int newValue)
    {
        type = newType;
        value = newValue;

        if(!buffImage) buffImage = transform.Find("Image").GetComponent<Image>();
        if(!buffNameText) buffNameText = transform.Find("StatName").GetComponent<TextMeshProUGUI>();
        if(!buffValueText) buffValueText = transform.Find("StatValue").GetComponent<TextMeshProUGUI>();

        //buffImage.sprite =
        switch (type)
        {
            case ITEM_BUFFTYPE.Attack:
                buffNameText.text = "공격력";
                break;
            case ITEM_BUFFTYPE.Defence:
                buffNameText.text = "방어력";
                break;
            case ITEM_BUFFTYPE.Health:
                buffNameText.text = "최대체력";
                break;
            case ITEM_BUFFTYPE.Critical:
                buffNameText.text = "치명타";
                break;
        }
        buffValueText.text = value.ToString();
    }
}
