using Gamelogic.Extensions;
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

    public void StartCurrentLevel()
    {
        Game.Instance.StartLevel(Stages[currentStage].Levels[currentLevel]);
    }

    public void OnLevelPassed()
    {
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
            return;
        }

        StartCurrentLevel();
    }

    public void OnLevelFailed()
    {

    }


    /// <summary>
    /// Translate the Swipe gesture from the mouse to a direction for the player
    /// </summary>
    /// <param name="direction"></param>
    public void OnDirection(int direction)
    {
        if(Game.Instance.State != GameState.LevelActive)
        {
            return;
        }
        var gridDirection = Vector3.forward;
        switch ((Direction)direction)
        {
            case Direction.Up:
                gridDirection = Vector3.back;
                break;
            case Direction.Right:
                gridDirection = Vector3.left;
                break;
            case Direction.Down:
                gridDirection = Vector3.forward;
                break;
            case Direction.Left:
                gridDirection = Vector3.right;
                break;
        }

        // Try to move the player
        Runner.Instance.SetDirection(gridDirection);
    }
}

public enum Direction { Up = 0, Right, Down, Left }
