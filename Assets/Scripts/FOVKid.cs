using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVKid : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask, obstructionMask;

    public bool canSeeCuco;

    private SoundManager _soundMan;
    //public GameObject kidRef;

    PlayerController player;



    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        _soundMan = FindObjectOfType<SoundManager>();
        StartCoroutine(FOVRoutine());
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

    public bool getBoolean()
    {
        return canSeeCuco;
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
                player.addSuspicion(30);
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


