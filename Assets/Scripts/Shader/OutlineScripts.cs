using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScripts : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;

    private Renderer outlineRenderer;

    private void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        //outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scale, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlineObject.transform.localScale = new Vector3(1f,1f,-1f);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scale);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineScripts>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;


        return rend;
    }

    public void Disable()
    {
        outlineRenderer.enabled = false;
    }

    public void Enable()
    {
        outlineRenderer.enabled = true;
    }
}
