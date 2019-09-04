using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Level config data")]
public class LevelConfig : BaseLevelConfig
{
    public int[] Tiles;
    public int targetX;
    public int targetY;
    public int startX;
    public int startY;



    public Vector2Int targetPosition { get { return new Vector2Int(targetX, targetY); } }
    public Vector2Int startPosition { get { return new Vector2Int(startX, startY); } }

    public void Init()
    {
        targetX = WidthCells - 1;
        targetY = HeightCells - 1;
        startX = 0;
        startY = 0;

        Tiles = new int[HeightCells * WidthCells];

        Tiles[Index(targetX, targetY)] = (int)TileState.Target;
        Tiles[Index(startX, startY)] = (int)TileState.Start;
    }

    public void SetValue(int column, int row, int tileState)
    {
       
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
