using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerShot : MonoBehaviour
{
    PlayerInput Input;
    [field: SerializeField] public Gun EquipedGun { get; private set; }
    [field: SerializeField] public Camera PlayerVC {  get; private set; }

    IEnumerator ShotCoroutine;
    
    void Start()
    {
        Input = GetComponent<PlayerInput>();
        ShotCoroutine = null;
        StartCoroutine(InitGunSet());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.PlayerActions.Attack.IsPressed() && ShotCoroutine == null && EquipedGun.IsAmmo)
        {
            ShotCoroutine = Shot();
            StartCoroutine(ShotCoroutine);
        }
    }

    IEnumerator InitGunSet()
    {
        Gun tempGun = transform.Find("PlayerViewCam/Gun").GetComponent<Gun>();
        if (!tempGun)
        {
            tempGun = Instantiate(Resources.Load("Gun/Gun")).GetComponent<Gun>();
        }

        yield return new WaitUntil(() => UIManager.Instance.playerHUD != null);
        
        EquipGun(tempGun);
    }

    IEnumerator Shot()
    {
        EquipedGun.Shot();
        yield return new WaitForSeconds(EquipedGun.Data.gunData.FireRate);
        ShotCoroutine = null;
    }
    public void EquipGun(Gun newGun)
    {
        EquipedGun = newGun;
        UIManager.Instance.playerHUD.SetGun(EquipedGun);
    }
}