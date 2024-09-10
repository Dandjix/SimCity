using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainTypesBank))]
public class TerrainTypesBankEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(DrawDefaultInspector())
        {
            TerrainManager.Instance.Generate();
        }
    }
}
