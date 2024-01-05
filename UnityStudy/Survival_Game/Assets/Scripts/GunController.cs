using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static bool isActivate = false;
    [SerializeField] private Gun currentGun;//현재 장착된 총
    public Gun getCurrentGun { get { return currentGun; } }
    private float currentFireRate;//연사속도
    private bool isReload = false;//장전상태
    [HideInInspector] public bool isFineSightMode = false;//정조준상태
    //본래 포지션 값
    [SerializeField] private Vector3 originPos;
    //컴포넌트
    private AudioSource audioSource;
    [SerializeField] private Camera theCam;
    private Crosshair theCrosshair;
    //레이저 충돌정보
    private RaycastHit hitInfo;

    //피격이펙트
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
    //연사속도 재계산
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    //발사시도
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && isReload == false)
        {
            Fire();
        }
    }
    //발사 전 계산
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
    //발사 후 계산
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
    //명중 계산
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
    //재장전 시도
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
    //재장전 코루틴
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
    //정조준 취소
    public void CancelFineSight()
    {
        if (isFineSightMode) FineSight();
    }
    //정조준 시도
    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2") && isReload == false)
        {
            FineSight();
        }
    }
    //정조준 계산
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
    //정조준 활성화 코루틴
    IEnumerator FineSightAcivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    //정조준 비활성화 코루틴
    IEnumerator FineSightDeacivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(originPos, currentGun.transform.localPosition, 0.2f);
            yield return null;
        }
    }
    //반동 코루틴
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);
        if(isFineSightMode)
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;
            //반동시작
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }
            //반동원위치
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun.transform.localPosition = originPos;
            //반동시작
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }
            //반동원위치
            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
    }
    //효과음 재생
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
