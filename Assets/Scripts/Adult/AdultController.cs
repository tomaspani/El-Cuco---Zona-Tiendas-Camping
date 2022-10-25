using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultController : MonoBehaviour
{

    [SerializeField] float suspcionValue;
    [SerializeField] Material[] mat;
    private FieldOfView _fov;
    private WaypointMover _WM;


    private void Start()
    {
        //if(_fov == null)
        //{
        this.gameObject.GetComponent<MeshRenderer>().material = mat[0];
        _fov = this.GetComponentInParent<FieldOfView>();
        _WM = this.GetComponentInParent<WaypointMover>();
        //}
        
    }



    private void SeeThrough()
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

    private void FixedUpdate()
    {
        if (_fov.canSeePlayer == true && _fov.seesPlayer == false)
            AddSuspicion(_fov.playerRef);
        else if (_fov.canSeePlayer == true && _fov.seesPlayer == true)
            AddAlert(_fov.playerRef, 5f);
        else
        {
            LooseSuspicion(_fov.playerRef);
        }

        SeeThrough();
    }

    public void AddSuspicion(GameObject player)
    {
        player.GetComponent<PlayerController>().addSuspicion(suspcionValue * Time.deltaTime);
    }

    public void AddAlert(GameObject player, float multiplier)
    {
        player.GetComponent<PlayerController>().addSuspicion(25f * Time.deltaTime);
    }

    public void LooseSuspicion(GameObject player)
    {
        player.GetComponent<PlayerController>().LooseSuspicion(suspcionValue / 8f  * Time.deltaTime);
    }


    public void GoToKid(KidController kid)
    {
        _WM.GoToKid(kid);
    }


}
