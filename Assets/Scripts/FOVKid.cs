using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FOVKid : MonoBehaviour
{

    public float radius;
    [Range(0, 360)]
    public float angle;

    [SerializeField] float runAwayDistance;
    float distanceToPlayer;
    public float callAdultRadius;

    public LayerMask targetMask, obstructionMask, callMask;
    public bool canSeeCuco;
    bool hasSeenCuco;
    [Header("References")]
    private SoundManager _soundMan;
    //public GameObject kidRef;
    [SerializeField] KidType _kidType;
    public PlayerController player;
    NavMeshAgent _nvm;
    KidController kid;
    Transform adult;

    public bool canCallAdult;


    private void Start()
    {
        _nvm = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
        _soundMan = FindObjectOfType<SoundManager>();
        kid = GetComponent<KidController>();
        StartCoroutine(FOVRoutine());
        StartCoroutine(FOVCallAdultRoutine());
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch (_kidType)
        {
            case KidType.Brave:
                if (canCallAdult && canSeeCuco)
                {
                    CallAdult(kid);
                }
                break;
            case KidType.Coward:
                if (canSeeCuco || hasSeenCuco)
                {
                    RunAway();
                }
               
                    break;

            case KidType.Glutton:
                break;
        }
      
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            
        }
    }

    private IEnumerator FOVCallAdultRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCallAdult();

        }
    }
    #region SeeCuco
    private void FieldOfViewCheck()
    {
        Collider[] CucoRangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (CucoRangeChecks.Length != 0)
        {
            Transform KidTarget = CucoRangeChecks[0].transform;
            Vector3 directionToTarget = (KidTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, KidTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeCuco = true;
                    Debug.Log("aaaaah un cuco");
                    

                }
                else
                    canSeeCuco = false;
            }
            else
                canSeeCuco = false;

        }
        else
            canSeeCuco = false;
    }
    #endregion
    #region CallAdult
    private void FieldOfViewCallAdult()
    {
        Collider[] CallAdultCheck = Physics.OverlapSphere(transform.position, callAdultRadius, callMask);

        if(CallAdultCheck.Length != 0)
        {
            adult = CallAdultCheck[0].transform;
            Vector3 directionToTarget = (adult.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, adult.position);
            if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget))
            {
                canCallAdult = true;
            }
            else
            {
                canCallAdult = false;
            }
        }
        else
        {
            canCallAdult = false;
        }
    }

    private void CallAdult(KidController kid)
    {
        adult.GetComponent<AdultController>().GoToKid(kid);
    }

    public bool getBoolean()
    {
        return canSeeCuco;
    }

    public Transform getAdultTransform()
    {
        return adult;
    }
    #endregion
    #region RunAway

    void RunAway()
    {
        if (distanceToPlayer > runAwayDistance)
        {
            hasSeenCuco = false;
        }
        else
        { hasSeenCuco = true; }
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPosition = transform.position + dirToPlayer;
        _nvm.SetDestination(newPosition);
      
        
    }
    
    #endregion


    #region CheckeoSFX
    //----------------------------------------CHEQUEO SFX-----------------------------------------------------------------------------------

    bool checkitCanSeeCuco;

    void Update()
    {

        if (canSeeCuco != checkitCanSeeCuco)
        {
            checkitCanSeeCuco = canSeeCuco;

            if (canSeeCuco == true)
            {
                _soundMan.PlaySound("kidScream");
            }
            //else
                //_soundMan.StopSound("kidScream");
        }
    }


        //private void Update()
        //{
        //    if (canSeeCuco == true)
        //    {
        //        Debug.Log("Puedo secuestrar");
        //        if (Input.GetKeyDown(KeyCode.Alpha1))
        //        {
        //            player.SnatchKid(kidRef.GetComponent<KidController>());
        //            Debug.Log("te secuestre");
        //        }
        //    }
        //}
    }
#endregion
enum KidType
{
    Coward,
    Brave,
    Glutton
}

