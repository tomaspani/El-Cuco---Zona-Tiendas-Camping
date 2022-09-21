using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidController : MonoBehaviour
{
    public Text feedback;
    public bool isKidnapable;

    private void Update()
    {
        //feedback.text = "";
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isKidnapable = true;
            feedback.text = "Press 'K' to kidnap";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isKidnapable = true;
            feedback.text = "Press 'K' to kidnap";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isKidnapable = false;
            feedback.text = "";
        }
        
    }


    public void kidnap()
    {
        Debug.Log("me secuestraron lol");
        feedback.text = "";
        Destroy(this.gameObject);
    }
}
