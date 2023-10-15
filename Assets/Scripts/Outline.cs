using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] private Material outlineMateria;
    [SerializeField] private float outlineScaleFactor;
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
        outlineRenderer.transform.rotation = transform.rotation * Quaternion.Euler(1, 1, 180);
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObj = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObj.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetVector("_Scale", outlineObj.transform.localScale = Vector3.one * -scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObj.GetComponent<Outline>().enabled = false;
        outlineObj.GetComponent<Collider>().enabled = false;

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
