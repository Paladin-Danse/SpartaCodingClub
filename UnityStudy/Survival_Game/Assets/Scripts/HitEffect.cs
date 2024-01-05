using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private ParticleSystem myParticle;

    private void Start()
    {
        myParticle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (myParticle.isPlaying == false) gameObject.SetActive(false);
    }
}
