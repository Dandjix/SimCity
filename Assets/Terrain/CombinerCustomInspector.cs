
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Combiner))]
public class CombinerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Combiner combiner = (Combiner)target;
        //GUILayout.Label("combiner custom label"!);
        if(GUILayout.Button("Combine"))
        {
            combiner.Combine();
        }
    }
}
