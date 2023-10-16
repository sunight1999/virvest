using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    ParticleSystem particleSystem;
    bool isGrab;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        isGrab = false;
    }

    void Update()
    {
        OnOffParticle();
    }

    void OnOffParticle()
    {
        if (isGrab && (transform.eulerAngles.x < 300 && transform.eulerAngles.x > 270))
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }

    public void OnGrab()
    {
        isGrab = true;
    }

    public void OffGrab()
    {
        isGrab = false;
    }
}
