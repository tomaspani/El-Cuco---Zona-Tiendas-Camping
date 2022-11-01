using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsFollowAdult : MonoBehaviour
{
    [SerializeField] GameObject Adult;
    private WaypointMover wps;

    private void Start()
    {
        wps = Adult.GetComponent<WaypointMover>();
    }
    private void FixedUpdate()
    {
        if (!wps.IsLookingAround) 
        { transform.position = Adult.transform.position; transform.rotation = Adult.transform.rotation; }
        
    }
}
