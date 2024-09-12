using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HeightSetter))]
public class HeightSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(DrawDefaultInspector())
        {
            HeightSetter setter = (HeightSetter)target;
            setter.GenerateBrushTextureMaterial();
            setter.AdjustSize();
        }
    }
}
