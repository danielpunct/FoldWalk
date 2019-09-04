using Gamelogic.Extensions;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StageLevelsData[] Stages;

    int currentStage = 0;
    int currentLevel = 0;

    private void Start()
    {
        Game.Instance.ResetForMenu();
    }

    public void StartCurrentLevel(bool afterPass)
    {
        Game.Instance.StartLevel(Stages[currentStage].Levels[currentLevel], afterPass);
    }

    public IEnumerator OnLevelPassed()
    {
        Game.Instance.ShowLevelPassed();

        // go to next level
        if (currentLevel < Stages[currentStage].Levels.Length - 1)
        {
            currentLevel++;
        }
        // or go to next stage
        else if (currentStage < Stages.Length - 1)
        {
            currentStage++;
            currentLevel = 0;
        }
        // game finishsed
        else
        {
            yield break;
        }


        yield return new WaitForSeconds(2);

        StartCurrentLevel(true);
    }

    public IEnumerator OnLevelFailed()
    {
        Game.Instance.ShowLevelFail();

        yield return new WaitForSeconds(2);

        StartCurrentLevel(false);
    }
}