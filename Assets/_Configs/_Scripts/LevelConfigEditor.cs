using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfig), true)]
public class LevelConfigEditor : Editor
{
    public Color[] colors = new[] { Color.green, Color.red, Color.magenta, Color.cyan };


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
                var stateIndex = levelTarget.GetValue(col, row);
                string label = "";
                switch (stateIndex)
                {
                    case 1:
                        label = "";
                        break;
                    case 2:
                        label = "T";
                        break;
                    case 3:
                        label = "S";
                        break;
                }
                GUI.color = colors[stateIndex];
                if (GUILayout.Button(label, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    levelTarget.SetValue(col, row, LevelConfig.GetNextTileState(levelTarget.GetValue(col, row)));
                }
                GUI.color = precolor;
            }

            EditorGUILayout.EndVertical();
        }
        if (GUILayout.Button("Clear"))
        {
            levelTarget.Init();
        }

        EditorGUILayout.HelpBox(" Legend \n\n" +
           "Green: Free space \n" +
           "Red: Obstacle ( O ) \n" +
           "Magenta: Target position ( T )\n" +
           "Cyan: Start position ( S )", MessageType.Info);


        EditorUtility.SetDirty(serializedObject.targetObject);

        if(!CheckValidity())
        {
            EditorGUILayout.HelpBox(" NOT VALID \n\n" +
                "(Only) one START and one TARGET needed", MessageType.Error);
        }


        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

        bool CheckValidity()
        {
            bool startFound = false;
            bool targetFound = false;

            for (int row = 0; row < LevelConfig.HeightCells; row++)
            {
                for (int col = LevelConfig.WidthCells - 1; col >= 0; col--)
                {
                    var current = levelTarget.GetValue(col, row);
                    if (current == (int)TileState.Start)
                    {
                        if (startFound) return false;

                        levelTarget.startX = col;
                        levelTarget.startY = row;

                        startFound = true;
                    }
                    if (current == (int)TileState.Target)
                    {
                        if (targetFound) return false;

                        levelTarget.targetX = col;
                        levelTarget.targetY = row;

                        targetFound = true;
                    }
                }
            }
            return startFound && targetFound;
        }
    }

}
