using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEntities : MonoBehaviour
{
    public float radius;
   

    public LayerMask targetMask;

   

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] FindEntitiesCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

       
        if (FindEntitiesCheck.Length != 0)
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                foreach (Collider entity in FindEntitiesCheck)
                {
                    checkEntity(entity).ActivateSeeThrough();
                    //checkEntity(entity).isActivated = false;
                }
            }
            
        }
       
    }


    private SeeThrough checkEntity(Collider n)
    {
        if (n.GetComponent<SeeThrough>() != null)
        {
            return n.GetComponent<SeeThrough>();
        }
        else
        {
            return n.GetComponentInChildren<SeeThrough>();
        }
    }


    
}
