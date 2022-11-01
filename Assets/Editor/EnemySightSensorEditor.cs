using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnemySightSensor))]
public class EnemySightSensorEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemySightSensor ess = (EnemySightSensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ess.transform.position, Vector3.up, Vector3.forward, 360, ess.SusRadius);

        Vector3 viewSusAngleA = DirectionFromAngle(ess.transform.eulerAngles.y, -ess.angle / 2);
        Vector3 viewSusAngleB = DirectionFromAngle(ess.transform.eulerAngles.y, ess.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(ess.transform.position, ess.transform.position + viewSusAngleA * ess.SusRadius);
        Handles.DrawLine(ess.transform.position, ess.transform.position + viewSusAngleB * ess.SusRadius);

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
