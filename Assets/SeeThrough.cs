using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{

    [SerializeField] private Material[] mat;

    private void Update()
    {
        Activate();
    }

    private void Activate()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat[1];
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat[0];
        }
    }
}
