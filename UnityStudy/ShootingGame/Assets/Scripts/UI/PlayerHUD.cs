using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    Gun CurrentGun;

    Text MaxAmmoText;
    Text AmmoText;
    Text WeaponNameText;
    private void Start()
    {
        WeaponNameText = transform.Find("Components/Weapon/Weapon Name").GetComponent<Text>();
        MaxAmmoText = transform.Find("Components/Weapon/Weapon Clip Count").GetComponent<Text>();
        AmmoText = transform.Find("Components/Weapon/Weapon Bullet Count").GetComponent<Text>();
    }

    public void SetGun(Gun newGun)
    {
        CurrentGun = newGun;
        WeaponNameText.text = CurrentGun.Data.gunData.GunName;
        MaxAmmoText.text = CurrentGun.Data.gunData.MaxAmmo.ToString();
        AmmoText.text = CurrentGun.Ammo.ToString();
    }

    public void UseAmmoUpdate()
    {
        AmmoText.text = CurrentGun.Ammo.ToString();
    }

    public void UnEquipWeapon()
    {
        CurrentGun = null;
        WeaponNameText.text = "No Weapon";
        MaxAmmoText.text = "0";
        AmmoText.text = "0";
    }
}
