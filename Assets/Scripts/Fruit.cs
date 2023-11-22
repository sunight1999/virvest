using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit: MonoBehaviour
{
    public void FreezInactive()
    {
        transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
