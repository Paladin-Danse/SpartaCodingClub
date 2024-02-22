using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "GunData")]
public class GunSO : ScriptableObject
{
    [field: SerializeField] public GunData gunData { get; private set; }
}
