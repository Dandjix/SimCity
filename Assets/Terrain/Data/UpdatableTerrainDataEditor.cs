using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpdatableTerrainData),true)]
public class UpdatableTerrainDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var updatable = (UpdatableTerrainData)target;
        if(DrawDefaultInspector() && updatable.autoUpdate)
        {
            TerrainManager.Instance.Generate();
        }

        if(GUILayout.Button("Generate"))
        {
            TerrainManager.Instance.Generate();
        }
    }
}
