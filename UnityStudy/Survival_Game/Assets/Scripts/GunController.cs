using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static bool isActivate = false;
    [SerializeField] private Gun currentGun;//���� ������ ��
    public Gun getCurrentGun { get { return currentGun; } }
    private float currentFireRate;//����ӵ�
    private bool isReload = false;//��������
    [HideInInspector] public bool isFineSightMode = false;//�����ػ���
    //���� ������ ��
    [SerializeField] private Vector3 originPos;
    //������Ʈ
    private AudioSource audioSource;
    [SerializeField] private Camera theCam;
    private Crosshair theCrosshair;
    //������ �浹����
    private RaycastHit hitInfo;

    //�ǰ�����Ʈ
    [SerializeField] private GameObject hit_effect_prefab;
    private List<GameObject> effectList = new List<GameObject>();
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        theCrosshair = FindObjectOfType<Crosshair>();

        originPos = Vector3.zero;
        if(effectList.Count > 0) effectList.Clear();

        //WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        //WeaponManager.currentWeaponAnim = currentGun.anim;
    }

    void Update()
    {
        if (isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }
    }
    //����ӵ� ����
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    //�߻�õ�
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && isReload == false)
        {
            Fire();
        }
    }
    //�߻� �� ���
    private void Fire()
    {
        if (isReload == false)
        {
            if (currentGun.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }
    //�߻� �� ���
    private void Shoot()
    {
        theCrosshair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }
    //���� ���
    private void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward +
            new Vector3(UnityEngine.Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy),
                        UnityEngine.Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy), 0),
            out hitInfo, currentGun.range))
        {
            effectPooling(hit_effect_prefab, hitInfo);
        }
    }
    //������ �õ�
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) &&
            isReload == false &&
            currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }
    //������ �ڷ�ƾ
    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;

            currentGun.anim.SetTrigger("Reload");
            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;
        }
        else
        {

        }
    }
    //������ ���
    public void CancelFineSight()
    {
        if (isFineSightMode) FineSight();
    }
    //������ �õ�
    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2") && isReload == false)
        {
            FineSight();
        }
    }
    //������ ���
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);
        theCrosshair.FineSightAnimation(isFineSightMode);
        if(isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightAcivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeacivateCoroutine());
        }
    }
    //������ Ȱ��ȭ �ڷ�ƾ
    IEnumerator FineSightAcivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    //������ ��Ȱ��ȭ �ڷ�ƾ
    IEnumerator FineSightDeacivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(originPos, currentGun.transform.localPosition, 0.2f);
            yield return null;
        }
    }
    //�ݵ� �ڷ�ƾ
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);
        if(isFineSightMode)
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;
            //�ݵ�����
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }
            //�ݵ�����ġ
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun.transform.localPosition = originPos;
            //�ݵ�����
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }
            //�ݵ�����ġ
            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
    }
    //ȿ���� ���
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
    private void effectPooling(GameObject mEffect, RaycastHit mHitInfo)
    {
        foreach(GameObject effect in effectList)
        {
            if(effect.activeSelf == false)
            {
                effect.transform.position = mHitInfo.point;
                effect.transform.rotation = Quaternion.LookRotation(mHitInfo.normal);
                effect.SetActive(true);
                return;
            }
        }
        GameObject obj = Instantiate(mEffect, mHitInfo.point, Quaternion.LookRotation(mHitInfo.normal));
        effectList.Add(obj);
    }

    public void GunChange(Gun _gun)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate = true;
    }
}
