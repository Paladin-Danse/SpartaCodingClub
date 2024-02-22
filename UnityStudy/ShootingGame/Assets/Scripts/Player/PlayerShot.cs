using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerShot : MonoBehaviour
{
    PlayerInput Input;
    [field: SerializeField] public GunShot GunShot {  get; private set; }
    [field: SerializeField] public Camera PlayerVC {  get; private set; }

    IEnumerator ShotCoroutine;
    
    void Start()
    {
        Input = GetComponent<PlayerInput>();
        ShotCoroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.PlayerActions.Attack.IsPressed() && ShotCoroutine == null)
        {
            ShotCoroutine = Shot();
            StartCoroutine(ShotCoroutine);
        }
    }

    IEnumerator Shot()
    {
        GunShot.Shot();
        yield return new WaitForSeconds(GunShot.Data.gunData.FireRate);
        ShotCoroutine = null;
    }
}
