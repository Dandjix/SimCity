using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TerrainManager))]
public class TerrainTilerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();



        TerrainManager tiler = (TerrainManager)target;

        if (DrawDefaultInspector() && tiler.autoUpdate)
        {
            tiler.Generate();
        }

        if (GUILayout.Button("Generate"))
        {
            tiler.Generate();
        }
    }
}
