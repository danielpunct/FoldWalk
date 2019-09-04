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
    Resting, 
    Walking
}

public enum GameState
{
    InMenu,
    LevelActive
}