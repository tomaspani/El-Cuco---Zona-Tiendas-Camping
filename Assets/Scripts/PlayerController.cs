using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const float maxSus = 100f;
    const float maxKidSus = 45f;

    float limitKidSus;

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

    public void addKidSuspicion(float val)
    {
        float x = val * Time.deltaTime;

        if (suspicion < (maxSus * 0.9f))
        {
            if(limitKidSus < maxKidSus)
            {
                suspicion += x;
                limitKidSus += x;
            }
            else
            {
                
            }
        }
        //else
            //suspicion = maxSus * 0.9f;
        
    }

    public void LooseSuspicion(float val)
    {
        suspicion -= val;
        limitKidSus = 0;
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
