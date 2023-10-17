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
        Debug.Log(transform.rotation.x);
        OnOffParticle();
    }

    void OnOffParticle()
    {
        if (isGrab && (transform.eulerAngles.x < 320 && transform.eulerAngles.x > 250) &&
            (transform.eulerAngles.y > 0 && transform.eulerAngles.y < 360))
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
