using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public PlayerStatus status;

    TextMeshProUGUI playerNameText;
    TextMeshProUGUI playerLevel;
    Slider playerExp;
    TextMeshProUGUI playerDescription;

    private void Start()
    {
        playerNameText = transform.Find("nameBG/Name").GetComponent<TextMeshProUGUI>();
        playerLevel = transform.Find("LvExp/Lv").GetComponent<TextMeshProUGUI>();
        playerExp = playerLevel.transform.Find("Exp").GetComponent<Slider>();
        playerDescription = transform.Find("Description").GetComponent<TextMeshProUGUI>();

        playerNameText.text = status.playerName;
        playerLevel.text = "Lv : " + status.playerLevel.ToString();
        playerExp.value = status.playerExp > 0 ? status.playerExp / status.playerNextLvExp : 0;
        playerDescription.text = status.playerDescription;
    }
}
