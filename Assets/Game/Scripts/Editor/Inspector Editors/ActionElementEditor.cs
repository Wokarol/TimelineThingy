using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActionElement), true)]
[CanEditMultipleObjects]
public class ActionElementEditor : Editor
{


    private void OnSceneGUI()
    {

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnDisable()
    {

    }
}