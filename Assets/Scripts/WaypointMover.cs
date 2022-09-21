using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;

    [SerializeField] private float movSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;

    private Transform currentWaypoint;

    private FieldOfView _fov;


    private void Start()
    {
        currentWaypoint = waypoints.getNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.transform.position;

        currentWaypoint = waypoints.getNextWaypoint(currentWaypoint);

        _fov = this.GetComponent<FieldOfView>();
        
    }

    private void FixedUpdate()
    {
        if (_fov.seesPlayer!)
        {
            SeesPlayer();
        }
        else { 
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, movSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                currentWaypoint = waypoints.getNextWaypoint(currentWaypoint);
                var targetRotation = Quaternion.LookRotation(currentWaypoint.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
                //transform.LookAt(currentWaypoint);
                endWaypoint();
            }
        }
        CanSeePlayer();
        
        
    }

    private void CanSeePlayer()
    {
        
        if (_fov.canSeePlayer)
        {
            //transform.LookAt(_fov.playerRef.transform);
            //movSpeed = 0.1f;
            var targetRotation = Quaternion.LookRotation(_fov.playerRef.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
            movSpeed = 0f;
        }
        else { 
            movSpeed = 2.5f;
            var targetRotation = Quaternion.LookRotation(currentWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
            //transform.LookAt(currentWaypoint);
        }
    }

    private void SeesPlayer()
    {
        if (_fov.seesPlayer)
        {
            movSpeed = 1.5f;
            _fov.alertRadius = -_fov.susRadius;
            var targetRotation = Quaternion.LookRotation(_fov.playerRef.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _fov.playerRef.transform.position, movSpeed * Time.deltaTime);
        }
        else
        {
            movSpeed = 2.5f;
            var targetRotation = Quaternion.LookRotation(currentWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
            //transform.LookAt(currentWaypoint);
        }
    }


    private void endWaypoint()
    {
        //
    }

    
}
