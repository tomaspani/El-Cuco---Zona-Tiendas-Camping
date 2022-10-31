using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public Transform Player { get; private set; }

    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private float _Range;
    private Ray _ray;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    public bool Ping()
    {
        if (Player == null)
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

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_ray.origin, _ray.origin + _ray.direction * _Range);
    }
}