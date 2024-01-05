using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //컴포넌트
    [SerializeField] private GunController theGunController;
    private Gun currentGun;
    //HUD 온오프
    [SerializeField] GameObject go_BulletHUD;
    //탄약 갯수 텍스트 반영
    [SerializeField] private Text[] text_Bullet;

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.getCurrentGun;
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
