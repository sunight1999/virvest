using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : MonoBehaviour
{
    public FarmManager farmManager;

    public GameObject compostPrefab;

    ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Farmland")
        {
            ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

            for(int i = 0; i < collisionEvents.Count; i++)
            {
                if (farmManager.compostIndex < 10)
                {
                    GameObject c = Instantiate(compostPrefab, collisionEvents[i].intersection, Quaternion.identity);
                    farmManager.farmland.composts.Add(c);
                    farmManager.compostIndex++;
                }
                else
                {
                    farmManager.compostIndex = 0;
                    other.layer = 10;
                    return;
                }
            }
        }
    }
}
