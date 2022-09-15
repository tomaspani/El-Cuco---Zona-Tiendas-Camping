using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float suspicion;
    public int kidnappedKids; 
    public Text text;


    void Kidnap (KidController kid)
    {
        kidnappedKids++;
        kid.kidnap();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "kid")
        {
            Debug.Log("hola soy un nene");
            if (Input.GetKeyDown(KeyCode.Mouse1) && other.GetComponent<KidController>().isKidnapable)
            {
                Debug.Log("a que no me secuestras xd");
                Kidnap(other.GetComponent<KidController>());
            }
        }
        
    }

   

    public void addSuspicion(float val)
    {
        suspicion += val;
    }

    public void looseSuspicion(float val)
    {
        suspicion -= val;
        if(suspicion < 0f)
        {
            suspicion = 0f;
        }
    }


    

}
