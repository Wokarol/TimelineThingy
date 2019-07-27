using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActionElement), true)]
[CanEditMultipleObjects]
public class ActionElementEditor : Editor
{
    bool preview = false;
    bool playing = false;
    int frameCount = 0;
    float progress = 0.5f;

    private void OnSceneGUI()
    {
        if (playing)
            Repaint();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        ActionElement element = ((ActionElement)target);
        if (GUILayout.Button("Preview")) {
            preview = !preview;
            if (preview) {
                element.StartPreview(0);
                element.PreviewEvaluate(progress);
            } else {
                element.EndPreview();
                playing = false;
            }
        }

        if (GUILayout.Button(">")) {
            playing = !playing;
            if (playing) {
                frameCount = 0;
            }
            if (!preview) {
                preview = true;
                progress = 0;
                element.StartPreview(0);
                element.PreviewEvaluate(progress);
            }
            if(!playing && progress > 1) {
                progress = 1;
            }
        }

        if (playing) {
            if (frameCount > 4) {
                progress += Time.deltaTime / element.Duration;
            } else {
                frameCount++;
            }
            if(progress > 1 + ( 0.5f / element.Duration)) {
                progress = 0;
            }
            element.PreviewEvaluate(progress);
        }

        if (!preview || playing) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Slider(Mathf.Clamp01(progress), 0, 1);
            EditorGUI.EndDisabledGroup();
        } else {
            EditorGUI.BeginChangeCheck();
            progress = EditorGUILayout.Slider(progress, 0, 1);

            if (EditorGUI.EndChangeCheck()) {
                element.PreviewEvaluate(progress);
            }

            EditorGUI.EndDisabledGroup();
        }

        EditorGUILayout.EndHorizontal();

        if (preview)
            EditorGUILayout.HelpBox("PREVIEW is active", MessageType.Warning);

        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck() && preview) {
            element.PreviewEvaluate(progress);
        }
    }

    private void OnDisable()
    {
        if (preview) {
            ((ActionElement)target).EndPreview();
            playing = false;
            preview = false;
        }
    }
}