using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidController : MonoBehaviour
{
    public Text feedback;
    public bool isKidnapable;
    public GameObject candy;
    public GameObject candyDrop;

    private FOVKid _fov;
    private bool _canSeeCuco;

    private void Start()
    {
        _fov = GetComponent<FOVKid>();
    }

    private void Update()
    {
        //feedback.text = "";
        _canSeeCuco = _fov.getBoolean();

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _canSeeCuco == false)
        {
            isKidnapable = true;
            feedback.text = "Press '1' to kidnap";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _canSeeCuco == false)
        {
            isKidnapable = true;
            feedback.text = "Press '1' to kidnap";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            isKidnapable = false;
            feedback.text = "";
        }
        
    }


    public void kidnap()
    {
        if (isKidnapable)
        {
            Debug.Log("me secuestraron lol");
            feedback.text = "";
            Destroy(this.gameObject);
        }
        
    }

    public void SpawnCandy()
    {
        Instantiate(candy, candyDrop.transform.position, Quaternion.identity);
    }
}
