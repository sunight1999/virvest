using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : MonoBehaviour
{
    public FarmManager farmManager;

    public GameObject compostPrefab;
    public ParticleSystem compostParticle;
    int compostIndex;

    ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents;

    bool isReadyComposting;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        isReadyComposting = true;
    }

    private void Update()
    {
        Composting();
    }

    List<Vector3> tests = new List<Vector3>();
    private void OnParticleCollision(GameObject other)
    {
        //TimeManager.Instance.Day
        if(other.gameObject.tag == "Farmland")
        {
            ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

            for(int i = 0; i < collisionEvents.Count; i++)
            {
                if (compostIndex < 10 && other.GetComponent<Ground>().composts.Count < 10)
                {
                    if (collisionEvents[i].intersection.Equals(Vector3.zero))
                        return;

                    GameObject c = Instantiate(compostPrefab, collisionEvents[i].intersection, Quaternion.identity);
                    farmManager.farmland.composts.Add(c);
                    compostIndex++;
                }
                else
                {
                    compostIndex = 0;
                    other.layer = 10;
                    isReadyComposting = false;
                    return;
                }
            }
        }
    }

    public void Composting()
    {
        if (isReadyComposting && farmManager.isGrab && (transform.eulerAngles.z > 160 && transform.eulerAngles.z < 200))
        {
            compostParticle.Play();
        }
        else if (isReadyComposting == false && !(transform.eulerAngles.z > 160 && transform.eulerAngles.z < 200))
        {
            isReadyComposting = true;
        }
        else
            compostParticle.Stop();
    }

}
