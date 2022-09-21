using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultController : MonoBehaviour
{

    [SerializeField] float suspcionValue;
    private FieldOfView _fov;


    private void Start()
    {
        //if(_fov == null)
        //{
            _fov = this.GetComponent<FieldOfView>();
        //}
        
    }

    private void FixedUpdate()
    {
        if (_fov.canSeePlayer == true && _fov.seesPlayer == false)
            AddSuspicion(_fov.playerRef);
        else if (_fov.canSeePlayer == true && _fov.seesPlayer == true)
            AddAlert(_fov.playerRef, 5f);
        else
            LooseSuspicion(_fov.playerRef);
        
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
        player.GetComponent<PlayerController>().looseSuspicion(suspcionValue / 2f  * Time.deltaTime);
    }


}
