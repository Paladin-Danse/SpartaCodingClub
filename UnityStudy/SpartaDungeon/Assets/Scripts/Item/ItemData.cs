using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    Equipable,
    Consumable
}
[CreateAssetMenu(fileName = "Item", menuName = "new Item")]
public class ItemData : ScriptableObject
{
    [Header("Default")]
    public string name;
    public bool stackable;
    public int stack;
    public int maxStack;
    public Sprite sprite;
    public string description;
    public ITEM_TYPE itemType;

    [Header("Equipment")]
    public int atk;
    public int def;
    public int hp;
    public int crit;
}