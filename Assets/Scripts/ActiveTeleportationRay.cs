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

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        leftDirectController.SetActive(!isLeftRayHovering && leftActive.action.ReadValue<float>() == 0);

        bool isRightRayHovering = leftRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
        rightDirectController.SetActive(!isRightRayHovering && rightActive.action.ReadValue<float>() == 0);

        leftController.SetActive(!isLeftRayHovering && leftActive.action.ReadValue<float>() > 0.1f);
        rightController.SetActive(!isRightRayHovering && rightActive.action.ReadValue<float>() > 0.1f);
    }
}
