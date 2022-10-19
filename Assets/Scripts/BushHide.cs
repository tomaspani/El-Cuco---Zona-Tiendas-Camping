using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushHide : MonoBehaviour
{
    private int hidden = 7;
    private int notHidden = 8;
    private PlayerController playerRef;

    private void Start()
    {
        playerRef = FindObjectOfType<PlayerController>();
        //Debug.Log(playerRef);
    }


    /*private void OnTriggerEnter(Collider other)
    {
        this.gameObject.layer = hidden;
    }*/

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(playerRef);
        if(other.gameObject.tag == "bush")
        {
            //other.gameObject.layer = hidden;
            playerRef.isHidden = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(playerRef);
        if (other.gameObject.tag == "bush")
        {
            //other.gameObject.layer = notHidden;
            playerRef.isHidden = false;
        }
        
    }
}
