using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;//���� �̸�
    public float range;//�� ��Ÿ�
    public float accuracy;//���߷�
    public float fireRate;//����ӵ�
    public float reloadTime;//������ �ӵ�
    public int damage;//������
    public int reloadBulletCount;//�������Ǵ� ź�� ��
    public int currentBulletCount;//���� ź�� ��
    public int maxBulletCount;//�ִ� ź�� ��
    public int carryBulletCount;//���� ����ִ� ź���� ��

    public float retroActionForce;//�ݵ�����
    public float retroActionFineSightForce;//�����ؽ� �ݵ�����

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
