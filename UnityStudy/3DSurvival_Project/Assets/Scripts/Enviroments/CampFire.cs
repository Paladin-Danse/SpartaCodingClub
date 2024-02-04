using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private HashSet<IDamageable> thingsToDamage = new HashSet<IDamageable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        foreach(IDamageable damageable in thingsToDamage)
        {
            damageable.TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            thingsToDamage.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            thingsToDamage.Remove(damageable);
        }
    }
}
