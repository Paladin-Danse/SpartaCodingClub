using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField] public string playerName { get; private set; }
    [field: SerializeField] public int playerLevel { get; private set; }
    [field: SerializeField] public int playerExp { get; private set; }
    [field: SerializeField] public int playerNextLvExp { get; private set; }
    public string playerDescription;
    
    [field: SerializeField] public int attackPoint { get; private set; }
    [field: SerializeField] public int defencePoint { get; private set; }
    [field: SerializeField] public int maxHP { get; private set; }
    [field: SerializeField] public int criticalPoint { get; private set; }
    public int currentHP { get; private set; }

    private void Start()
    {
        currentHP = maxHP;
    }
}
