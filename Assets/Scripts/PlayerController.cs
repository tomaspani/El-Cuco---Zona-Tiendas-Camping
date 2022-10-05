using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float suspicion;
    public int kidnappedKids;
    public int kidsInBag;
    public Text text;
    public bool isHidden;

    public void SnatchKid(KidController kid)
    {
        kidnappedKids++;
        kidsInBag++;
        if(kidnappedKids == 1)
        {
            kid.SpawnCandy();
        }
        kid.kidnap();
    }

    public void addSuspicion(float val)
    {
        suspicion += val;
    }

    public void looseSuspicion(float val)
    {
        suspicion -= val;
        if(suspicion < 0f)
        {
            suspicion = 0f;
        }
    }

    public void Consume()
    {
        kidsInBag = 0;
    }

    public Vector3 lastPosition()
    {
        return this.transform.position;

    }

}
