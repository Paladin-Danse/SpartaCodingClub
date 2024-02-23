using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotImpact : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    public void OnEnable()
    {
        particleSystem?.Play();
    }

    private void Update()
    {
        if (particleSystem?.isPlaying == false) DisableParticle();
    }

    public void DisableParticle()
    {
        particleSystem?.gameObject.SetActive(false);
    }
}
