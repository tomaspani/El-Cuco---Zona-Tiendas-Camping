using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class FOVCuco : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask, obstructionMask;

    public bool canSnatchKid;

    public GameObject kidRef;

    PlayerController player;



    private void Start()
    {
        player = GetComponent<PlayerController>();
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
        Collider[] KidRangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (KidRangeChecks.Length != 0)
        {
            Transform KidTarget = KidRangeChecks[0].transform;
            Vector3 directionToTarget = (KidTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, KidTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSnatchKid = true;
                    kidRef = KidTarget.gameObject;
                }
                else
                    canSnatchKid = false;
            }
            else
                canSnatchKid = false;

        }
        else
            canSnatchKid = false;
    }



    private void Update()
    {
        if(canSnatchKid == true)
        {
            Debug.Log("Puedo secuestrar");
            if (Input.GetKeyDown(KeyCode.K)){
                player.SnatchKid(kidRef.GetComponent<KidController>());
                Debug.Log("te secuestre");
            }
        }
    }
}
