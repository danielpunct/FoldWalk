using System;
using System.Collections.Generic;
using UnityEngine;


// idle = 0, walked = 1, obstacle = 2, start = 3, finish = 4
public class LevelGenerator
{
    static int rowsNr = 5;
    static int columnsNr = 5;
    static byte[,] mat;

    public static bool GetLevel(Vector2Int startPosition, out LevelConfig levelConfig)
    {
        rowsNr = LevelConfig.HeightCells;
        columnsNr = LevelConfig.WidthCells;
        mat = new byte[columnsNr, rowsNr];

        var start = new v2( startPosition.x, startPosition.y);
        mat[start.x, start.y] = 3;
        var isOk = true;
        var cursor = start;
        var lastDir = new v2(0, 0);
        var laststop = new v2(-1, -1);

        var maxTurns = 5;

        var rnd = new System.Random();
        while (true) // another corner
        {
            var final = rnd.Next(4) == 0 || maxTurns-- == 0;
            if (!SetNextStop(cursor, out cursor, out var dir, final))
            {
                if (lastDir.x == -1 || !SetEndableValidPosition(laststop, lastDir, true))
                {
                    isOk = false;
                }

                break;
            }

            lastDir = dir;
            laststop = cursor;

            if (final)
            {
                break;
            }
        }

#if UNITY_EDITOR
        Debug.Log(isOk ? "ok" : "fail");
        display();
#endif

        levelConfig = ParseToLevel();
        return isOk;
    }

    static LevelConfig ParseToLevel()
    {
        var level = new LevelConfig();
        level.Init();

        for (int row = 0; row < LevelConfig.HeightCells; row++)
        {
            for (int col = LevelConfig.WidthCells - 1; col >= 0; col--)
            {
                TileState state;
                switch (mat[col, row])
                {
                    case 2:
                        state = TileState.Obstacle;
                        break;
                    case 3:
                        state = TileState.Start;
                        level.startX = col;
                        level.startY = row;
                        break;
                    case 4:
                        state = TileState.Target;
                        level.targetX = col;
                        level.targetY = row;
                        break;
                    case 0:
                    case 1:
                    default:
                        state = TileState.Free;
                        break;

                }
                level.SetValue(col, row, (int)state);
            }
        }

        return level;
    }


    static bool SetNextStop(v2 from, out v2 cursor, out v2 dir, bool final)
    {
        cursor = new v2(0, 0);
        if (!GetAvailableRandomDirection(from, out dir))
        {
#if UNITY_EDITOR
            Debug.Log("no direction");
#endif

            return false;
        }

        cursor = from;
        var rnd = new System.Random();

        var min = 1;
        while (min > 0 || rnd.Next(0, 5) > 0) // go forth
        {
            min--;
            if (SetPassablePosition(cursor + dir))
            {
                cursor += dir;
            }
            else
            {
                break;
            }
        }

        if (SetEndableValidPosition(cursor, dir, final))
        {
            return true;
        }

        return false;
    }

    static bool GetAvailableRandomDirection(v2 from, out v2 dir)
    {
        var directions = new List<v2>
            {
                new v2(1, 0),
                new v2(0, 1),
                new v2(-1, 0),
                new v2(0, -1)
            };

        var rnd = new System.Random();
        while (directions.Count > 0)
        {
            var idx = rnd.Next(directions.Count);

            dir = directions[idx];

            directions.RemoveAt(idx);

            var newPos = from + dir;

            // if outside bounds
            if (newPos.x < 0 || newPos.x >= columnsNr ||
                newPos.y < 0 || newPos.y >= rowsNr)
            {
                continue;
            }

            // if is occupied
            if (mat[newPos.x, newPos.y] != 0)
            {
                continue;
            }

            return true;

        }

        dir = new v2(0, 0);
        return false;
    }


    static bool SetPassablePosition(v2 pos)
    {
        if (pos.x < 0 || pos.x >= columnsNr ||
            pos.y < 0 || pos.y >= rowsNr)
        {
            return false;
        }

        // if is occupied
        if (mat[pos.x, pos.y] == 2)
        {
            return false;
        }

        if (mat[pos.x, pos.y] == 0)
        {
            mat[pos.x, pos.y] = 1;
        }

        return true;
    }

    static bool SetEndableValidPosition(v2 pos, v2 dir, bool final)
    {
        if (mat[pos.x, pos.y] == 3)
        {
            return false;
        }

        var nextPos = pos + dir;

        // if is blocked or already out side - nothing to do
        if (SetPassablePosition(nextPos))
        {
            // if start is next we cannot put blocker
            if (mat[nextPos.x, nextPos.y] == 3)
            {
                return false;
            }

            mat[nextPos.x, nextPos.y] = 2;
        }


        if (final)
        {
            mat[pos.x, pos.y] = 4;
        }

        return true;
    }

    static void display()
    {
        for (var line = 0; line < rowsNr; line++)
        {
            string lineString = "";
            for (var column = 0; column < columnsNr; column++)
            {
                lineString += mat[column, line] + " ";
            }
            Debug.Log(lineString);
        }
    }

     struct v2
    {
        public int x;
        public int y;

        public v2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public static v2 operator +(v2 a, v2 b)
        {
            return new v2(a.x + b.x, a.y + b.y);
        }


        public static v2 operator -(v2 a, v2 b)
        {
            return new v2(a.x - b.x, a.y - b.y);
        }
    }
}
