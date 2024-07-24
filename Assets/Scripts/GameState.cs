using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    public enum States
    {
        Playing, Paused, Lose
    }

    public States currentState;

    public bool isPlaying()
    {
        return currentState == States.Playing;
    }

    public bool isGameOver()
    {
        return currentState == States.Lose;
    }
}
