using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class Outline : MonoBehaviour
{
    [SerializeField] private Material outlineMateria;
    [SerializeField] private Vector3 outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    Renderer outlineRenderer;

    

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMateria, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = false;
    }

    void Update()
    {
        outlineRenderer.transform.position = transform.position;
        outlineRenderer.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
    }

    Renderer CreateOutline(Material outlineMat, Vector3 scaleFactor, Color color)
    {
        GameObject outlineObj = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObj.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetVector("_Scale", outlineObj.transform.localScale = -1*transform.localScale - scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObj.GetComponent<Outline>().enabled = false;
        outlineObj.GetComponent<Collider>().enabled = false;
        outlineObj.GetComponent<XRGrabInteractable>().enabled = false;

        rend.enabled = false;

        return rend;
    }

    public void OnHoverEnter()
    {
        outlineRenderer.enabled = true;
    }

    public void OnHoverExit()
    {
        outlineRenderer.enabled = false;
    }
}
