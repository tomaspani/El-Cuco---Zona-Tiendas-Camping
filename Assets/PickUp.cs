using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private OutlineScripts outline;

    private void Start()
    {
        outline = GetComponent<OutlineScripts>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.Enable();
            Debug.Log("podes agarrarme");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.Disable();
            Debug.Log("mefuis");

        }
    }
}
