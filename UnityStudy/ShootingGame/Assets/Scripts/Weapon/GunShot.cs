using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField] public Transform shotPos { get; private set; }
    public GunSO Data;
    LayerMask layerMask;

    ParticleSystem gunShotImpact;
    List<ParticleSystem> particleSystems;
    private void Start()
    {
        shotPos = transform.GetChild(0).Find("GunPos");
        layerMask = 1 << LayerMask.NameToLayer("Default");
        gunShotImpact = Resources.Load("Prefebs/ParticleEffects/gunShotImpact").GetComponent<ParticleSystem>();
        particleSystems = new List<ParticleSystem>();
    }

    public void Shot()
    {
        RaycastHit hit;
        Vector3 aimCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, Data.gunData.Distance);
        Vector3 hitPoint;
        Vector3 screenRayAddDistance = transform.forward * (transform.localPosition.z + shotPos.localPosition.z);
        Ray screenRay = new Ray(Camera.main.ScreenPointToRay(aimCenter).origin + screenRayAddDistance, Camera.main.ScreenPointToRay(aimCenter).direction);
        if (Physics.Raycast(screenRay, out hit, Mathf.Infinity, layerMask))
        {
            hitPoint = hit.point;
            if (Physics.Raycast(shotPos.position, hitPoint - shotPos.position, out hit, Data.gunData.Distance, layerMask))
            {
                Debug.Log("Hit");
                gunShotImpactPooling(gunShotImpact, hit);
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

    public void gunShotImpactPooling(ParticleSystem obj, RaycastHit hit)
    {
        foreach(ParticleSystem impact in particleSystems)
        {
            if(impact.gameObject.activeSelf == false)
            {
                impact.transform.position = hit.point;
                impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                impact.gameObject.SetActive(true);
                impact.Play();
                return;
            }
        }
        ParticleSystem newImpact = Instantiate(obj.gameObject, hit.point, Quaternion.LookRotation(hit.normal)).GetComponent<ParticleSystem>();
        newImpact.Play();
        particleSystems.Add(newImpact);
    }
}
