using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    public FarmManager farmManager;

    ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
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
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Farmland")
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
