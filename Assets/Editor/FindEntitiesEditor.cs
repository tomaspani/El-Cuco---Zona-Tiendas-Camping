using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FindEntities))]
public class FindEntitiesEditor : Editor
{
    private void OnSceneGUI()
    {
        FindEntities fe = (FindEntities)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fe.transform.position, Vector3.up, Vector3.forward, 360, fe.radius);
    }
}