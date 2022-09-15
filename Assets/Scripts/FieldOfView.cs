using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float susRadius;
    public float alertRadius;
    [Range(0,360)]
    public float angle;

    

    public GameObject playerRef;

    public LayerMask targetMask, obstructionMask;

    public bool canSeePlayer, seesPlayer = false;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
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
        Collider[] susRangeChecks = Physics.OverlapSphere(transform.position, susRadius, targetMask);

        if (susRangeChecks.Length != 0)
        {
            Transform susTarget = susRangeChecks[0].transform;
            Vector3 directionToTarget = (susTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, susTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    if (alertRadius < susRadius * 0.9f)
                    {
                        alertRadius += 35f * Time.deltaTime;
                    }
                    alertFieldOfViewCheck();
                }
                else
                {
                    canSeePlayer = false;
                    seesPlayer = false;
                    alertRadius = 2.5f;
                }
            }
            else
            {
                canSeePlayer = false;
                seesPlayer = false;
                alertRadius = 2.5f;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            seesPlayer = false;
            alertRadius = 2.5f;
        }
            


        
    }


    private void alertFieldOfViewCheck()
    {
        Collider[] alertRangeChecks = Physics.OverlapSphere(transform.position, alertRadius, targetMask);
        if (alertRangeChecks.Length != 0)
        {
            Transform alertTarget = alertRangeChecks[0].transform;
            Vector3 directionToTarget = (alertTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, alertTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    seesPlayer = true;
                }

                else
                    seesPlayer = false;
            }
            else
               seesPlayer = false;
        }
        else if (seesPlayer)
            seesPlayer = false;

    }
}
