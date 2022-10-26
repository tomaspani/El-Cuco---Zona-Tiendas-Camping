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
    [SerializeField] float counter = 0;
    [SerializeField] AdultType type;
    Vector3 startPosition;

    //[SerializeField] private Waypoints waypoints;

    [SerializeField] private float movSpeed = 5f;

    private Vector3 lastPosition;

    float count;
    public float timeToLook;

    private void Start()
    {
        _fov = this.GetComponent<FieldOfView>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeWaypoint();
        _navMeshAgent.isStopped = false;
        transform.position = _waypoints[0].position;
        currentWatchPosition = _positionsToCheck[0];
        startPosition = transform.position;
        
        
    }
    #region FixedUpdate
    private void FixedUpdate()
    {
        var distanceToWaypoint = Vector3.Distance(_currentWaypoint.position, transform.position);
        
        switch (type)
        {
            case AdultType.Guard:
                
                if (_fov.seesPlayer!)
                {
                    SeesPlayer();
                }
                else if (getsCalled == false)
                {
                    count = 0f;
                    _navMeshAgent.isStopped = false;
                    //Debug.Log("lol");
                    if (distanceToWaypoint <= _distanceToCheck)
                    {
                        IsLookingAround = true;
                        ChangeLookPosition();
                        //Invoke(nameof(ChangeLookPosition), _lookingTime);


                        if (!IsLookingAround)
                        {

                            ChangeWaypoint();
                        }

                    }
                }
                else
                {
                    count += Time.fixedDeltaTime;
                    if (count >= timeToLook)
                    {
                        getsCalled = false;
                    }
                }

                CanSeePlayer();

                break;
            case AdultType.Watcher:
                if (_fov.seesPlayer!)
                {
                    SeesPlayer();
                }
                else if (distanceToWaypoint <= _distanceToCheck)
                {
                    Debug.Log("entra en else if");
                    _navMeshAgent.isStopped = true;
                    WatcherLookPositions();

                }
                else
                {
                    Debug.Log(distanceToWaypoint);
                    _navMeshAgent.isStopped = false;
                    ReturnToWatchPost();
                }
                CanSeePlayer();
                break;

        }
 


    }
    #endregion
    #region GotoKid
    public void GoToKid(KidController kid)
    {
        float distanceToTarget = Vector3.Distance(transform.position, kid.transform.position);
        var targetRotation = Quaternion.LookRotation(kid.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1000f * Time.fixedDeltaTime);
        if (distanceToTarget > 2f)
        {
            getsCalled = true;
            transform.position = Vector3.MoveTowards(transform.position, kid.transform.position, movSpeed * Time.fixedDeltaTime);
            _navMeshAgent.isStopped = true;
        }
        //else
        //{
        //    getsCalled = false;
        //}
        //getsCalled = false;


    }
    #endregion

    //DONE
    #region DetectPlayer
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

    void ChangeWaypoint()
    {
        //Debug.Log(_currentWaypointIndex);
        //Debug.Log(_waypoints.Count);
        if (getsCalled == false)
        {
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
        }  


    }
    private void endWaypoint()
    {
        //
    }

   
    
    void ChangeLookPosition ()
    {

        IsLookingAround = true;
        _navMeshAgent.isStopped = true;     
        currentWatchPosition = _positionsToCheck[_currentWatchPositionIndex];
        CheckPositionRotation = Quaternion.LookRotation(currentWatchPosition.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, CheckPositionRotation, 2.5f * Time.deltaTime);
        

        
        counter += Time.deltaTime;
        if (counter >= _lookingTime)
        {
            _currentWatchPositionIndex += 1;
            counter = 0;
        }
        if (_currentWatchPositionIndex >= _positionsToCheck.Count)
        {
            IsLookingAround = false;
            _currentWatchPositionIndex = 0;
        }
    }

    void ReturnToWatchPost ()
    {
        _navMeshAgent.SetDestination(startPosition);
    }

    void WatcherLookPositions ()
    {
        IsLookingAround = true;
        currentWatchPosition = _positionsToCheck[_currentWatchPositionIndex];
        CheckPositionRotation = Quaternion.LookRotation(currentWatchPosition.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, CheckPositionRotation, 2.5f * Time.deltaTime);
        counter += Time.deltaTime;
        if (counter >= _lookingTime)
        {
            _currentWatchPositionIndex += 1;
            counter = 0;
        }
        if (_currentWatchPositionIndex >= _positionsToCheck.Count)
        {
          
            _currentWatchPositionIndex = 0;
        }

    }
}
enum AdultType
{
    Guard,
    Watcher
}