using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushHide : MonoBehaviour
{
    private int hidden = 7;
    private int notHidden = 8;
    

    /*private void OnTriggerEnter(Collider other)
    {
        this.gameObject.layer = hidden;
    }*/

    private void OnTriggerStay(Collider other)
    {
        other.gameObject.layer = hidden;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = notHidden;
    }
}
