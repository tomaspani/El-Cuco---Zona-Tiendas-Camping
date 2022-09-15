using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertFeedback : MonoBehaviour
{
    public Image feedback;
    public Sprite[] images;

    FieldOfView fov;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.canSeePlayer == true && fov.seesPlayer == false)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[0];
        }
        else if (fov.canSeePlayer == true && fov.seesPlayer == true)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[1];
        }
        else
        {

            var tempColor = feedback.color;
            tempColor.a = 0f;
            feedback.color = tempColor;
            feedback.sprite = null;
        }
            

    }
}
