using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public Transform Player { get; private set; }

    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private LayerMask _targetMask;

    private Ray _ray;

    [SerializeField] private float _susRadius;
    public float SusRadius { get { return _susRadius; } }

    [Range(0, 360)]
    public float angle;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    public bool Ping()
    {
        Collider[] susRangeChecks = Physics.OverlapSphere(transform.position, _susRadius, _targetMask);

        if (susRangeChecks.Length != 0)
        {
            Transform susTarget = susRangeChecks[0].transform;
            Vector3 directionToTarget = (susTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, susTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _ignoreMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;

  //}
        /*if (Player == null)
            return false;

        _ray = new Ray(this.transform.position, Player.position - this.transform.position);

        if (Vector3.Dot(_ray.direction, this.transform.forward) < 0)
            return false;

        if (!Physics.Raycast(_ray, out var hit, _Range, ~_ignoreMask))
        {
            return false;
        }

        if (hit.collider.tag == "Player")
        {
            return true;
        }

        return false;*/
    }

}