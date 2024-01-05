using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;//총의 이름
    public float range;//총 사거리
    public float accuracy;//명중률
    public float fireRate;//연사속도
    public float reloadTime;//재장전 속도
    public int damage;//데미지
    public int reloadBulletCount;//재장전되는 탄약 수
    public int currentBulletCount;//현재 탄약 수
    public int maxBulletCount;//최대 탄약 수
    public int carryBulletCount;//현재 들고있는 탄약의 수

    public float retroActionForce;//반동세기
    public float retroActionFineSightForce;//정조준시 반동세기

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;

    public void WalkingAnimation(bool _flag)
    {
        anim.SetBool("Walk", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        anim.SetBool("Run", _flag);
    }
    public void FineSightAnimation(bool _flag)
    {
        anim.SetBool("FineSightMode", _flag);
    }
}
