using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    static private GameState currentInstance;
    static public GameState GetGameState() { return GameState.currentInstance; }

    public bool[] unlockedDoors = new bool[] { true, true, true, false };
}