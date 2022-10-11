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
    public List<Transform> _positionsToCheck = new List<Transform>();
    [SerializeField]Transform currentWatchPosition;
    public float _lookingTime;
    [SerializeField] int _currentWatchPositionIndex;
    public bool IsLookingAround;
    [SerializeField] private bool getsCalled;
    Quaternion CheckPositionRotation;
    float counter = 0;

    //[SerializeField] private Waypoints waypoints;

    [SerializeField] private float movSpeed = 5f;

    private Vector3 lastPosition;



    private void Start()
    {
        _fov = this.GetComponent<FieldOfView>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeWaypoint();
        _navMeshAgent.isStopped = false;
        transform.position = _waypoints[0].position;
        currentWatchPosition = _positionsToCheck[0];
      
        
    }

    private void FixedUpdate()
    {
        Debug.Log(getsCalled);
        var distanceToWaypoint = Vector3.Distance(_currentWaypoint.position, transform.position);
        if (_fov.seesPlayer!)
        {
            SeesPlayer();
        }
        else if (getsCalled!)
        {
            _navMeshAgent.isStopped = false;
            if (distanceToWaypoint <= _distanceToCheck)
            {

                
                ChangeLookPosition();
                    //Invoke(nameof(ChangeLookPosition), _lookingTime);
                

                if (!IsLookingAround) 
                { 
                    ChangeWaypoint(); 
                }
               
            }
        }

        CanSeePlayer();
        


    }
    public void GoToKid(KidController kid)
    {
        var targetRotation = Quaternion.LookRotation(kid.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
        float distanceToTarget = Vector3.Distance(transform.position, kid.transform.position);
        if (distanceToTarget > _distanceToCheck)
        {
            getsCalled = true;
            
            transform.position = Vector3.MoveTowards(transform.position, kid.transform.position, movSpeed/4 * Time.deltaTime);
            _navMeshAgent.isStopped = true;
        }
        else
        {
            
            _navMeshAgent.isStopped = false;
        }
        


    }

    #region LookingAtPlayer
    //DONE
    private void CanSeePlayer()
    {
        
        if (_fov.canSeePlayer)
        {
            //transform.LookAt(_fov.playerRef.transform);
            //movSpeed = 0.1f;
            
            var targetRotation = Quaternion.LookRotation(_fov.playerRef.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _fov.playerRef.transform.position, movSpeed/6f * Time.deltaTime);
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
    #endregion
    #region Waypoints
    void ChangeWaypoint()
    {
        //Debug.Log(_currentWaypointIndex);
        //Debug.Log(_waypoints.Count);
        //if (getsCalled == false)
        //{
            if (_currentWaypointIndex < _waypoints.Count - 1)
            {
                _currentWaypointIndex += 1;
            }
            else
            {
                _currentWaypointIndex = 0;
            }

            _currentWaypoint = _waypoints[_currentWaypointIndex];
            _navMeshAgent.SetDestination(_currentWaypoint.position);
            var targetRotation = Quaternion.LookRotation(_currentWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movSpeed * Time.deltaTime);
        //}  


    }
    private void endWaypoint()
    {
        //
    }

    #endregion
    #region LookPosition
    void ChangeLookPosition ()
    {
        Debug.Log("mirando a posicion");
        IsLookingAround = true;
        _navMeshAgent.isStopped = true;     
        currentWatchPosition = _positionsToCheck[_currentWatchPositionIndex];
        CheckPositionRotation = Quaternion.LookRotation(currentWatchPosition.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, CheckPositionRotation, 2.5f * Time.deltaTime);
        

        
        counter += Time.deltaTime;
        if (counter >= _lookingTime)
        {
            _currentWatchPositionIndex += 1;
            Debug.Log(_currentWatchPositionIndex);
            counter = 0;
        }
        if (_currentWatchPositionIndex >= _positionsToCheck.Count)
        {
            IsLookingAround = false;
            _currentWatchPositionIndex = 0;
        }
    }
    #endregion
}
