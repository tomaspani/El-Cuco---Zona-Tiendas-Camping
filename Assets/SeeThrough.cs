using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{

    [SerializeField] private Material[] mat;
    private float count = 0f;

    private bool checkBool;

    public bool isActivated;

    private void Update()
    {

        count += Time.deltaTime;
        if (count > 2f)
        {

            isActivated = false;
        }

        Activate();

       /* if (isActivated != checkBool)
        {
            checkBool = isActivated;

            if(isActivated == true)
            {
                isActivated = false;
            }
        }*/
    }

    private void Activate()
    {
        if (isActivated)
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().material = mat[1];
        }
        else
        {
            count = 0;
            this.gameObject.GetComponent<SkinnedMeshRenderer>().material = mat[0];
        }
    }


    public void ActivateSeeThrough()
    {
        
        isActivated = true;
        count = 0;

    }

}
