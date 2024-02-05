using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    TextMeshProUGUI goldText;
    [SerializeField] PlayerInventory inventory;

    private void Start()
    {
        goldText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        goldText.text = inventory.gold.ToString();
    }
}
