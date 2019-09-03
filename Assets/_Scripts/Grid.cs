using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : Singleton<Grid>
{
    public int maxX = 4;
    public int maxY = 4;

    /// <summary>
    /// Go in the direction until we find the edge or obstacle in the obstacles matrix
    /// </summary>
    /// <param name="from"></param>
    /// <param name="direction"></param>
    public Vector2Int GetAvailablePosition(Vector2Int from, Vector2Int direction)
    {
        var newPos = from;
        while (IsPositionAvailable(newPos + direction))
        {
            newPos += direction;
        }
        return newPos;
    }

    /// <summary>
    /// Check for edge or obstacles
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    bool IsPositionAvailable(Vector2 position)
    {
        if (position.x < 0 ||
            position.x > maxX ||
            position.y < 0 ||
            position.y > maxY)
        {
            return false;
        }

        return true;
    }


}
