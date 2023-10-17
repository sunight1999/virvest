using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoPlayer_Move : MonoBehaviour
{
    public Transform player;
    float x = 0;
    float z = 0;
    int speed = 20;
    void Start()
    {
        player = GetComponent<Transform>();
    }

    void Update()
    {
        x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        z += Input.GetAxis("Vertical") * speed * Time.deltaTime;

        player.position = new Vector3 (x, player.position.y, z);
    }
}
