using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Level config data")]
public class LevelConfig : BaseLevelConfig
{
    public int[] Tiles;
    public Vector2Int targetPosition;
    public Vector2Int startPosition;

    public void Init()
    {
        targetPosition = new Vector2Int(WidthCells - 1, HeightCells - 1);
        startPosition = new Vector2Int(0,0);
        Tiles = new int[HeightCells * WidthCells];

        Tiles[Index(targetPosition.x, targetPosition.y)] = (int)TileState.Target;
        Tiles[Index(startPosition.x, startPosition.y)] = (int)TileState.Start;
    }

    public void SetValue(int column, int row, int tileState)
    {
        if (tileState == (int)TileState.Target)
        {
            targetPosition = new Vector2Int(column, row);
        }
        if (tileState == (int)TileState.Start)
        {
            startPosition = new Vector2Int(column, row);
        }

        Tiles[Index(column, row)] = tileState;
    }

    public int GetValue(int column, int row)
    {
        return Tiles[Index(column, row)];
    }

    int Index(int column, int row)
    {
        return row * WidthCells + column;
    }

    public static int GetNextTileState(int current)
    {
        return (current + 1) % Enum.GetValues(typeof(TileState)).Length;
    }
}
