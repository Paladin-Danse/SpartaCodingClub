using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField] public Transform shotPos { get; private set; }
    public GunSO Data;
    LayerMask layerMask;
    private void Start()
    {
        shotPos = transform.GetChild(0).Find("GunPos");
        layerMask = 1 << LayerMask.NameToLayer("Default");
    }

    public void Shot()
    {
        RaycastHit hit;
        Vector3 aimCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, Data.gunData.Distance);
        Vector3 hitPoint;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(aimCenter), out hit, Mathf.Infinity, layerMask))
        {
            hitPoint = hit.point;
            if (Physics.Raycast(shotPos.position, hitPoint - shotPos.position, Data.gunData.Distance, layerMask))
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log("No Hit");
            }    
        }
        else
        {
            hitPoint = Camera.main.ScreenToWorldPoint(aimCenter);
            Debug.Log("No Target");
        }

        Debug.DrawRay(shotPos.position, hitPoint - shotPos.position);
    }
}
