using Gamelogic.Extensions;
using UnityEngine;

public class Game : Singleton<Game>
{
    public TurnPage turnPage;
    public Runner runnerHolder;
    public Transform holeHolder;
    public Transform indicatorHolder;
    public Transform obstaclesHolder;
    public GameObject obstaclePrefab;

    public ParticleSystem winParticles;
    public ParticleSystem looseParticles;

    public GameState State { get; set; }

    public void OnPlayerHitTarget()
    {
        if (State == GameState.LevelActive)
        {
            StartCoroutine(GameManager.Instance.OnLevelPassed());
        }
    }

    public void OnPlayerHitPage()
    {
        if (State == GameState.LevelActive)
        {
            StartCoroutine(GameManager.Instance.OnLevelFailed());
        }
    }

    public void Suspend()
    {
        State = GameState.InMenu;
    }

    public void ResetForMenu()
    {
        State = GameState.InMenu;
        turnPage.ResetForMenu();
    }

    public void StartLevel(LevelConfig levelConfig, bool afterPass)
    {
        // place the runner in the start position from level config
        runnerHolder.Setup(levelConfig.startPosition, levelConfig);

        // place the hole and indicator in the target position from level config
        holeHolder.transform.localPosition = levelConfig.targetPosition.To3D();
        indicatorHolder.transform.localPosition = levelConfig.targetPosition.To3D();

        // clear any obstacles from previous level
        PoolManager.Instance.ObstaclesPool.DespawnAll();

        // place new obstacles as in the level config
        for (int row = 0; row < LevelConfig.HeightCells; row++)
        {
            for (int col = LevelConfig.WidthCells - 1; col >= 0; col--)
            {
                var stateIndex = levelConfig.GetValue(col, row);

                if(stateIndex == (int)TileState.Obstacle)
                {
                    var obstacle = PoolManager.Instance.ObstaclesPool.Spawn(Vector3.zero, Quaternion.identity, obstaclesHolder).GetComponent<Obstacle>();
                    obstacle.Reset();
                    obstacle.transform.localPosition = new Vector2Int(col, row).To3D();
                }
            }
        }

        turnPage.StartLevel(afterPass);

        // indicate the game is live
        State = GameState.LevelActive;
    }


    public void ShowLevelFail()
    {
        runnerHolder.SetForFail();
        looseParticles.Stop();
        looseParticles.Play();
    }

    public void ShowLevelPassed()
    {
        winParticles.Stop();
        winParticles.Play();
    }

    /// <summary>
    /// Translate the Swipe gesture from the mouse to a direction for the player
    /// </summary>
    /// <param name="direction"></param>
    public void OnDirection(int direction)
    {
        if(State != GameState.LevelActive)
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



