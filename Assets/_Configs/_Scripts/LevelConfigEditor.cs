using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfig), true)]
public class LevelConfigEditor : Editor
{

    public Color[] colors = new[] { Color.green, Color.red, Color.magenta };

    public override void OnInspectorGUI()
    {
        var levelTarget = serializedObject.targetObject as LevelConfig;

        if (levelTarget.Tiles == null)
        {
            levelTarget.Init();
        }

        var precolor = GUI.contentColor;
        EditorGUILayout.BeginVertical();
        for (int row = 0; row < LevelConfig.HeightCells; row++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int col = LevelConfig.WidthCells - 1; col >= 0; col--)
            {
                GUI.color = colors[levelTarget.GetValue(col, row)];
                if (GUILayout.Button("", GUILayout.Width(30), GUILayout.Height(30)))
                {
                    levelTarget.SetValue(col, row, (levelTarget.GetValue(col, row) + 1) % 3);
                    EditorUtility.SetDirty(serializedObject.targetObject);
                }
                GUI.color = precolor;
            }

            EditorGUILayout.EndVertical();
        }
        if (GUILayout.Button("Clear"))
        {
            levelTarget.Init();
            EditorUtility.SetDirty(serializedObject.targetObject);
        }

        EditorGUILayout.HelpBox("Legend \n" +
           "Green: Free space \n" +
           "Red: Obstacle\n" +
           "Magenta: Target position", MessageType.Info);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.EndHorizontal();
    }
}
