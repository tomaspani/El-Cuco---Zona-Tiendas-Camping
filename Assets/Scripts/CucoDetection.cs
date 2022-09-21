using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucoDetection : MonoBehaviour
{
    private FieldOfView _fov;

    private void FixedUpdate()
    {
        if (_fov.seesPlayer!)
        {
            SeesPlayer(5f);
        }
        else
        {
           
        }
        CanSeePlayer();


    }

    public void CanSeePlayer()
    {

        if (_fov.canSeePlayer)
        {

            transform.LookAt(_fov.playerRef.transform);
        }
    }

    public void SeesPlayer(float speed)
    {
        if (_fov.seesPlayer)
        {
            _fov.alertRadius = -_fov.susRadius;
            transform.LookAt(_fov.playerRef.transform);
            transform.position = Vector3.MoveTowards(transform.position, _fov.playerRef.transform.position, speed * Time.deltaTime);
        }
    }
}
