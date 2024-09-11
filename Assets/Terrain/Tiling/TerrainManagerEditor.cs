using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor
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

        if (GUILayout.Button("Clear"))
        {
            tiler.Clear();
        }
    }
}
