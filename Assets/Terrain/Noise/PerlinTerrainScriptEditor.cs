using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NoiseTerrainScript))]
public class NoiseTerrainScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NoiseTerrainScript terrain = (NoiseTerrainScript)target;

        //if(GUILayout.Button("ApplyGridBounds"))
        //{
        //    terrain.ApplyGridBounds();
        //}

        if (GUILayout.Button("Generate"))
        {
            terrain.Generate();
        }
    }
}
