using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fruit: MonoBehaviour
{
    public void FreezInactive()
    {
        transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
