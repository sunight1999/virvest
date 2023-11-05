using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinyl : MonoBehaviour
{
    public FarmManager farmManager;

    public Transform player;
    public Transform origin;
    public bool isConnect;
    float dist;

    void Start()
    {

    }

    void Update()
    {
        if (isConnect)
        {
            dist = Vector3.Distance(origin.position, player.position);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
            transform.position = (player.position + origin.position) / 2;
            transform.LookAt(origin);
        }
    }
}
