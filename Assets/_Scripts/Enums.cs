using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    Free = 0,
    Obstacle,
    Target,
    Start
}


public enum RunnerState
{
    Stopped,
    Idle, 
    Walking
}

public enum GameState
{
    InMenu,
    LevelActive
}
public enum Direction
{
    Up = 0,
    Right,
    Down,
    Left
}
