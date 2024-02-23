using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _Instance;
    public static UIManager Instance
    {
        get 
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<UIManager>();
            return _Instance;
        }
    }

    public PlayerHUD playerHUD { get; private set; }

    private void Start()
    {
        GameObject temp = GameObject.Find("HUDCanvas");
        if (!temp)
            temp = Instantiate(Resources.Load<GameObject>("UI/HUDCanvas"));

        playerHUD = temp.GetComponent<PlayerHUD>();
    }
}
