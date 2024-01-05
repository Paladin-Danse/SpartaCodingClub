using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/upgardeData", order = 1)]
public class upgradeData : ScriptableObject
{
    [SerializeField] Upgrade data;
    public Upgrade Data { get { return data; } }
    public void InputData(Upgrade m_data)
    {
        data = m_data;
    }
}
