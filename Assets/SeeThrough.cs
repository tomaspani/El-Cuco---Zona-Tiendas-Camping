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
            this.gameObject.GetComponent<MeshRenderer>().material = mat[1];
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat[0];
        }
    }


    public void ActivateSeeThrough()
    {
        Debug.Log(count);
        count += Time.fixedDeltaTime;
        if(count < 0.25f)isActivated = true;
        else
        {
            isActivated = false;
            count = 0f;
        }
        
    }

}
