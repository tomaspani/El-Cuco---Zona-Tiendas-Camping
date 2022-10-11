using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVKid : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;


    public float callAdultRadius;

    public LayerMask targetMask, obstructionMask, callMask;

    public bool canSeeCuco;

    private SoundManager _soundMan;
    //public GameObject kidRef;

    public PlayerController player;

    KidController kid;
    Transform adult;

    public bool canCallAdult;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        _soundMan = FindObjectOfType<SoundManager>();
        kid = GetComponent<KidController>();
        StartCoroutine(FOVRoutine());
        StartCoroutine(FOVCallAdultRoutine());
    }

    private void FixedUpdate()
    {
        if(canCallAdult)
        {
            CallAdult(kid);
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

    private void FieldOfViewCallAdult()
    {
        Collider[] CallAdultCheck = Physics.OverlapSphere(transform.position, callAdultRadius, callMask);

        if(CallAdultCheck.Length != 0)
        {
            adult = CallAdultCheck[0].transform;
            Debug.Log(adult.name);
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

    //----------------------------------------CHEQUEO SFX-----------------------------------------------------------------------------------

    bool checkitCanSeeCuco;

    void Update()
    {

        if (canSeeCuco != checkitCanSeeCuco)
        {
            checkitCanSeeCuco = canSeeCuco;

            print("canseeplayer has changed to: " + canSeeCuco);
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


