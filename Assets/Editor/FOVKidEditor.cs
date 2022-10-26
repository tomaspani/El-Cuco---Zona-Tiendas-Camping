using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FOVKid))]
public class FOVKidEditor : Editor
{
    private void OnSceneGUI()
    {
        FOVKid fovkid = (FOVKid)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fovkid.transform.position, Vector3.up, Vector3.forward, 360, fovkid.radius);
        Handles.color = Color.red;
        Handles.DrawWireArc(fovkid.transform.position, Vector3.up, Vector3.forward, 360, fovkid.callAdultRadius);

        Vector3 viewSusAngleA = DirectionFromAngle(fovkid.transform.eulerAngles.y, -fovkid.angle / 2);
        Vector3 viewSusAngleB = DirectionFromAngle(fovkid.transform.eulerAngles.y, fovkid.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fovkid.transform.position, fovkid.transform.position + viewSusAngleA * fovkid.radius);
        Handles.DrawLine(fovkid.transform.position, fovkid.transform.position + viewSusAngleB * fovkid.radius);



        if (fovkid.canCallAdult)
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(fovkid.transform.position, fovkid.getAdultTransform().position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
