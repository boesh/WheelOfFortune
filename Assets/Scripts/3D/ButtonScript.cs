using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public bool spin = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        spin = true;
    }

    private void OnMouseEnter()
    {
       meshRenderer.sharedMaterial.color = Color.red;
    }

    private void OnMouseExit()
    {
        
        meshRenderer.sharedMaterial.color = Color.cyan;
    }
}
