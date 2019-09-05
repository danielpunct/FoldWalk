using Gamelogic.Extensions;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StageLevelsData[] Stages;

    int currentStage = 0;
    int currentLevelIndex = 0;
    bool randomLevelGetter = false;
    LevelConfig currentLevel = null;

    LevelConfig GetNextLevel
    {
        get
        {
            // save last end position
            Vector2Int lastTarget = new Vector2Int(0, 0);
            if(currentLevel != null)
            {
                lastTarget = currentLevel.targetPosition;
                currentLevel = null;
            }

            if (!randomLevelGetter)
            {
                currentLevel = Stages[currentStage].Levels[currentLevelIndex];
            }
            else
            {
                if (LevelGenerator.GetLevel(lastTarget, out var level))
                {
                    currentLevel = level;
                }
            }

            return currentLevel;
        }
    }

    private void Start()
    {
        Game.Instance.ResetForMenu();
    }

    public void StartCurrentLevel(bool afterPass, bool isRandoms)
    {
        randomLevelGetter = isRandoms;

        if (afterPass)
        {
            currentLevel = GetNextLevel;
        }
        if (currentLevel == null)
        {
            // goto menu
        }

        Game.Instance.StartLevel(currentLevel, afterPass);
    }

    public IEnumerator OnLevelPassed()
    {
        Game.Instance.ShowLevelPassed();

        if (!randomLevelGetter)
        {
            // go to next level
            if (currentLevelIndex < Stages[currentStage].Levels.Length - 1)
            {
                currentLevelIndex++;
            }
            // or go to next stage
            else if (currentStage < Stages.Length - 1)
            {
                currentStage++;
                currentLevelIndex = 0;
            }
            // game finishsed
            else
            {
                yield break;
            }
        }

        yield return new WaitForSeconds(2);

        StartCurrentLevel(true, randomLevelGetter);
    }

    public IEnumerator OnLevelFailed()
    {
        Game.Instance.ShowLevelFail();

        yield return new WaitForSeconds(2);

        Menu.Instance.ShowRestartUI();
    }


    public void RestartLevel()
    {
        StartCurrentLevel(false, randomLevelGetter);
    }
}