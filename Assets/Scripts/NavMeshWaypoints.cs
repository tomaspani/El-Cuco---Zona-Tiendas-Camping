using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshWaypoints : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _currentWaypoint;
    private NavMeshAgent _navMeshAgent;
    public List<Transform> _waypoints = new List<Transform>();
    [SerializeField] int _currentWaypointIndex;
    private FieldOfView _fov;
    [SerializeField] Transform _target;
    [Header("Values")]
    public float _distanceToCheck;
    [SerializeField] private float movSpeed = 5f;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;
        transform.position = _waypoints[0].position;
        ChangeWaypoint();
    }

    private void Update()
    {
        var distanceToTarget = Vector3.Distance(_target.position, transform.position);
        var distanceToWaypoint = Vector3.Distance(_currentWaypoint.position, transform.position);

        if (distanceToWaypoint <= _distanceToCheck)
        {
            ChangeWaypoint();
        }
        

    }

    void ChangeWaypoint()
    {

        _currentWaypointIndex += 1;
        if (_currentWaypointIndex > _waypoints.Count)
        {
            _currentWaypointIndex = 0;
        }
        _currentWaypoint = _waypoints[_currentWaypointIndex];
        
        _navMeshAgent.SetDestination(_currentWaypoint.position);

    }
}
