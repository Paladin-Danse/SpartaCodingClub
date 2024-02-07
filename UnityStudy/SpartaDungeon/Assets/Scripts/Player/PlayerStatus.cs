using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: Header("PlayerInfo")]
    [field: SerializeField] public string playerName { get; private set; }
    [field: SerializeField] public int playerLevel { get; private set; }
    [field: SerializeField] public int playerExp { get; private set; }
    [field: SerializeField] public int playerNextLvExp { get; private set; }
    public string playerDescription;

    [Header("PlayerDefaultStat")]
    [SerializeField] private int defaultAttackPoint;
    public int buffAttackPoint = 0;
    public int attackPoint
    {
        get { return defaultAttackPoint + buffAttackPoint; }
    }

    [SerializeField] private int defaultDefencePoint;
    public int buffDefencePoint = 0;
    public int defencePoint
    {
        get { return defaultDefencePoint + buffDefencePoint; }
    }

    [SerializeField] private int defaultHealthPoint;
    public int buffHealthPoint = 0;

    public int maxHP
    {
        get { return defaultHealthPoint + buffHealthPoint; }
    }

    [SerializeField] private int defaultCriticalPoint;
    public int buffCriticalPoint = 0;
    public int criticalPoint
    {
        get { return defaultCriticalPoint + buffCriticalPoint; }
    }
    
    public int currentHP { get; private set; }

    private void Start()
    {
        currentHP = maxHP;
    }
}
