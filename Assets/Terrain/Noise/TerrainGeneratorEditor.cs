using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();



        TerrainGenerator terrain = (TerrainGenerator)target;

        if (DrawDefaultInspector() && terrain.autoUpdate)
        {
            terrain.Generate();
        }
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
