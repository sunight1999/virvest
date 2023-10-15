using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveTeleportationRay : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public GameObject leftDirectController;
    public GameObject rightDirectController;

    public InputActionProperty leftActive;
    public InputActionProperty rightActive;

    void Update()
    {
        leftController.SetActive(leftActive.action.ReadValue<float>() > 0.1f);
        rightController.SetActive(rightActive.action.ReadValue<float>() > 0.1f);
        leftDirectController.SetActive(leftActive.action.ReadValue<float>() == 0);
        rightDirectController.SetActive(rightActive.action.ReadValue<float>() == 0);
    }
}
