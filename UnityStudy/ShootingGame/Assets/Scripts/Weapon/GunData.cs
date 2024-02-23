using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunData
{
    [field: SerializeField] public string GunName;
    [field: SerializeField][field: Range(0, 100)] public int Damage { get; private set; }
    [field: SerializeField][field: Range(0, 100)] public int Accuracy { get; private set; }
    [field: SerializeField][field: Range(0, 100)] public int Recoil { get; private set; }
    [field: SerializeField][field: Range(0, 600)] public float Distance { get; private set; }
    [field: SerializeField][field: Range(0, 1)] public float FireRate { get; private set; }
    [field: SerializeField] public int MaxAmmo { get; private set; }
    public int Ammo { get; private set; }
}
