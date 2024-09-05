using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TerrainTiler))]
public class TerrainTilerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();



        TerrainTiler tiler = (TerrainTiler)target;

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
