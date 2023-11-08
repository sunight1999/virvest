using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    public FarmManager farmManager;

    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        OnOffParticle();
    }

    void OnOffParticle()
    {
        if (farmManager.isGrab && (transform.eulerAngles.x < 320 && transform.eulerAngles.x > 250) &&
            (transform.eulerAngles.y > 0 && transform.eulerAngles.y < 360))
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.layer >= 9)
        {
            Debug.Log("...");
            farmManager.WateringToSoil(other);
        }
    }
}
