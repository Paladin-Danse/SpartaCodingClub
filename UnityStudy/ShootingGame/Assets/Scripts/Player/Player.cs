using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerInput inputActions { get; set; }

    void Start()
    {
        inputActions = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
