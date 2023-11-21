using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : SingletonMono<PlayerStartPoint>
{
    public static PlayerStartPoint instance;
    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Instance.transform.localPosition = this.transform.localPosition;
            Destroy(gameObject);
        }

    }
}
