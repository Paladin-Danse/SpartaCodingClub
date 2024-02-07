using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    Equipable,
    Consumable
}

public enum ITEM_BUFFTYPE
{
    Attack,
    Defence,
    Health,
    Critical
}
[System.Serializable]
public class ItemDataBuffStat
{
    public ITEM_BUFFTYPE buffStat;
    public int buffValue;
}

[CreateAssetMenu(fileName = "Item", menuName = "new Item")]
public class ItemData : ScriptableObject
{
    [HideInInspector] public int index;
    [Header("Default")]
    public string name;
    public bool stackable;
    public int stack;
    public int maxStack;
    public Sprite sprite;
    public string description;
    public ITEM_TYPE itemType;

    [Header("Equipment")]
    public ItemDataBuffStat[] buff;
}