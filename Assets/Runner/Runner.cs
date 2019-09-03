using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Singleton<Runner>
{
    public void StartMove(Vector2 direction)
    {
        Debug.Log(direction);
    }
 }
