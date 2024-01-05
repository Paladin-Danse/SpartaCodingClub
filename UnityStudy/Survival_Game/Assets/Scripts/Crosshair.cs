using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    //������Ʈ
    [SerializeField] private Animator anim;

    //ũ�ν���� ���¿� ���� ���� ��Ȯ��
    private float gunAccuracy;
    //ũ�ν���� ��Ȱ��ȭ�� ���� �θ� ��ü.
    [SerializeField]
    private GameObject go_CrosshairHUD;
    [SerializeField]
    private GunController theGunController;
    public void WalkingAnimation(bool _flag)
    {
        anim.SetBool("Walk", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        anim.SetBool("Running", _flag);
    }
    public void CrouchingAnimation(bool _flag)
    {
        anim.SetBool("Crouching", _flag);
    }
    public void FineSightAnimation(bool _flag)
    {
        anim.SetBool("FineSight", _flag);
    }
    public void FireAnimation()
    {
        if(anim.GetBool("Walk"))
            anim.SetTrigger("Walk_Fire");
        else if(anim.GetBool("Crouching"))
            anim.SetTrigger("Crouch_Fire");
        else
            anim.SetTrigger("Idle_Fire");
    }

    public float GetAccuracy()
    {
        if (anim.GetBool("Walk"))
            gunAccuracy = 0.06f;
        else if (anim.GetBool("Crouching"))
            gunAccuracy = 0.015f;
        else if (theGunController.isFineSightMode)
            gunAccuracy = 0f;
        else
            gunAccuracy = 0.035f;

        return gunAccuracy;
    }
}
