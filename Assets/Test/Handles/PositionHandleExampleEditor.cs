using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionHandleExample)), CanEditMultipleObjects]
public class PositionHandleExampleEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        PositionHandleExample example = (PositionHandleExample)target;

        Vector3 newTargetPosition = Handles.PositionHandle(example.targetPosition, Quaternion.identity);
        example.targetPosition = newTargetPosition;
    }
}