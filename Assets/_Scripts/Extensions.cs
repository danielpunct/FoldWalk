using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2Int To2D(this Vector3 vector3D)
    {
        return new Vector2Int(Mathf.RoundToInt(vector3D.x), Mathf.RoundToInt(vector3D.z));
    }

    public static Vector3 To3D(this Vector2Int vector2D)
    {
        return new Vector3(vector2D.x, 0, vector2D.y);
    }
}
