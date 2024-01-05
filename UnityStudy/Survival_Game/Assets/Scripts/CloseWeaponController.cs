using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추상 클래스
public abstract class CloseWeaponController : MonoBehaviour
{
    //현재 장착된 Hand형 타입 무기
    [SerializeField] protected CloseWeapon currentCloseWeapon;
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;

    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (isAttack == false)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay -
                                        currentCloseWeapon.attackDelayA -
                                        currentCloseWeapon.attackDelayB);

        isAttack = false;
    }
    //미완성. 추상 코루틴
    protected abstract IEnumerator HitCoroutine();
        /*
    {
        while (isSwing)
        {
            if (checkObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
        */
    protected bool checkObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
            return true;
        return false;
    }

    //가상 함수. 완성함수, 추가가능
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
    public void WalkingAnimation(bool _flag)
    {
        Debug.Log(currentCloseWeapon.anim.name);
        currentCloseWeapon.anim.SetBool("Walk", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        currentCloseWeapon.anim.SetBool("Run", _flag);
    }
}
