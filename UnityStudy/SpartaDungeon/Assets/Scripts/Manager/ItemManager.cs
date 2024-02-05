using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public ItemData[] itemDatas;
    private void Awake()
    {
        instance = this;
    }
}
