using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public Runner runnerHolder;
    public Transform holeHolder;
    public Transform obstaclesHolder;
    public GameObject obstaclePrefab;


    public void SetupLevel(LevelConfig levelConfig)
    {
        runnerHolder.transform.localPosition = levelConfig.startPosition.To3D();
        holeHolder.transform.localPosition = levelConfig.targetPosition.To3D();

        obstaclesHolder.DestroyChildren();

        for (int row = 0; row < LevelConfig.HeightCells; row++)
        {
            for (int col = LevelConfig.WidthCells - 1; col >= 0; col--)
            {
                var stateIndex = levelConfig.GetValue(col, row);

                if(stateIndex == (int)TileState.Obstacle)
                {
                    var obstacle = Instantiate(obstaclePrefab, obstaclesHolder);
                    obstacle.transform.localPosition = new Vector2Int(col, row).To3D();
                }
            }
        }
    }
}
