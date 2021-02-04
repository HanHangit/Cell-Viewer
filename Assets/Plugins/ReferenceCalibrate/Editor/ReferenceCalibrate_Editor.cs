using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReferenceCalibrate))]
public class ReferenceCalibrate_Editor : Editor
{   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Apply current"))
            (target as ReferenceCalibrate).ApplyCurrentRegistration();

        if (GUILayout.Button("Apply from file"))
            (target as ReferenceCalibrate).ApplyRegistrationFromFile();

        if (GUILayout.Button("Save current to file"))
            (target as ReferenceCalibrate).SaveCurrentRegistrationToFile();

        GUILayout.EndHorizontal();
    }
}
