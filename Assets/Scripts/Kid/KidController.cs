using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidController : MonoBehaviour
{
    public Text feedback;
    public bool isKidnapable;
    public bool isKidnapableee;
    public GameObject candy;
    public GameObject candyDrop;

    public float susValue;

    private FOVKid _fov;
    private bool _canSeeCuco;
    private SoundManager _soundMan;
    private OutlineScript outline;
    private SeeThrough st;

    private void Start()
    {
        _fov = GetComponent<FOVKid>();
        _soundMan = FindObjectOfType<SoundManager>();
        outline = GetComponentInChildren<OutlineScript>();
        st = GetComponentInChildren<SeeThrough>();
    }

    private void FixedUpdate()
    {
        //feedback.text = "";
        _canSeeCuco = _fov.getBoolean();

        if (_canSeeCuco == true)
            SeeCuco();
        //else
        //cantSeeCuco();

        if (isKidnapableee)
        {
            st.enabled = false;
            outline.Enable();
        }
        else 
        {
            st.enabled = true;

            outline.Disable();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _canSeeCuco == false)
        {
            isKidnapable = true;
            isKidnapableee = true;
            //feedback.text = "Press Left Click to Kidnap";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _canSeeCuco == false)
        {
            isKidnapable = true;
            isKidnapableee = true;
            //feedback.text = "Press Left Click to Kidnap";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            isKidnapable = false;
            isKidnapableee = false;
            //feedback.text = "";
        }
        
    }

    public void SeeCuco()
    {

        var targetRotation = Quaternion.LookRotation(_fov.player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        _fov.player.addKidSuspicion(susValue);
        //transform.LookAt(_fov.player.transform.position);
        //Debug.Log("omg");
    }

    public void cantSeeCuco()
    {
        _fov.player.LooseSuspicion(susValue);

    }
    public void kidnap()
    {
        if (isKidnapable)
        {
            _soundMan.PlaySound("kidnap");
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
