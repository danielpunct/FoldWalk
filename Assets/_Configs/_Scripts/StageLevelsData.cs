using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage Levels Config")]
public class StageLevelsData : ScriptableObject
{
    public LevelConfig[] Levels;
}
