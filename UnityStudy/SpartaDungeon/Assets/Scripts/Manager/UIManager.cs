using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Transform UI_Buttons;
    public StatusUI statusPanel;
    public InventoryUI InventoryPanel;

    private void Awake()
    {
        instance = this;
    }

    public void setStatusPanel(bool setBool)
    {
        statusPanel.OnPanel(setBool);
        UI_Buttons.gameObject.SetActive(!setBool);
    }

    public void setInventoryPanel(bool setBool)
    {
        InventoryPanel.OnPanel(setBool);
        UI_Buttons.gameObject.SetActive(!setBool);
    }
}
