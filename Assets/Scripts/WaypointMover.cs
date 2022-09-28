using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointMover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _currentWaypoint;
    private NavMeshAgent _navMeshAgent;
    public List<Transform> _waypoints = new List<Transform>();
    [SerializeField] int _currentWaypointIndex;
    public FieldOfView _fov;
    [Header("Values")]
    public float _distanceToCheck;
    //[SerializeField] private Waypoints waypoints;

    [SerializeField] private float movSpeed = 5f;

    private Vector3 lastPosition;



    private void Start()
    {
        _fov = this.GetComponent<FieldOfView>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentWaypointIndex = 0;
        ChangeWaypoint();
        _navMeshAgent.isStopped = false;
        transform.position = _waypoints[0].position;
        
      
        
    }

    private void FixedUpdate()
    {
        var distanceToWaypoint = Vector3.Distance(_currentWaypoint.position, transform.position);
        if (_fov.seesPlayer!)
        {
            SeesPlayer();
        }
        else {
            _navMeshAgent.isStopped = false;
            if (distanceToWaypoint <= _distanceToCheck)
            {
                ChangeWaypoint();
                var targetRotation = Quaternion.LookRotation(_currentWaypoint.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
                //transform.LookAt(currentWaypoint);
                endWaypoint();
            }
        }
        CanSeePlayer();
        


    }
    //DONE
    private void CanSeePlayer()
    {
        
        if (_fov.canSeePlayer)
        {
            //transform.LookAt(_fov.playerRef.transform);
            //movSpeed = 0.1f;
            var targetRotation = Quaternion.LookRotation(_fov.playerRef.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
            _navMeshAgent.isStopped = true;
        }
        else {
            _navMeshAgent.isStopped = false;
            var targetRotation = Quaternion.LookRotation(_currentWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
            //transform.LookAt(currentWaypoint);
        }
    }

    private void SeesPlayer()
    {
        if (_fov.seesPlayer)
        {
            movSpeed = 1.5f;
            _navMeshAgent.isStopped = true;
            _fov.alertRadius = -_fov.susRadius;
            var targetRotation = Quaternion.LookRotation(_fov.playerRef.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _fov.playerRef.transform.position, movSpeed * Time.deltaTime);
            lastPosition = _fov.LastSeenPosition();
        }
        else
        {
            movSpeed = 2.5f;
            var targetRotation = Quaternion.LookRotation(_currentWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
            //transform.LookAt(currentWaypoint);
        }
    }



    void ChangeWaypoint()
    {
        Debug.Log(_currentWaypointIndex);
        Debug.Log(_waypoints.Count);
        if (_currentWaypointIndex < _waypoints.Count-1)
        {
            _currentWaypointIndex += 1;
        }
        else
        {
            _currentWaypointIndex = 0;
        }

        _currentWaypoint = _waypoints[_currentWaypointIndex];
        _navMeshAgent.SetDestination(_currentWaypoint.position);



    }
    private void endWaypoint()
    {
        //
    }

    
}
