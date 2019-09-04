using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Level config data")]
public class LevelConfig : BaseLevelConfig
{
    public int[] Tiles;
    public Vector2Int targetPosition;

    public void Init()
    {
        targetPosition = new Vector2Int(WidthCells - 1, HeightCells - 1);
        Tiles = new int[HeightCells * WidthCells];
    }

    public void SetValue(int column, int row, int tileState)
    {
        if (tileState == (int)TileState.Target)
        {
            Tiles[Index(targetPosition.x, targetPosition.y)] = (int)TileState.Free;
            targetPosition = new Vector2Int(column, row);
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
}
