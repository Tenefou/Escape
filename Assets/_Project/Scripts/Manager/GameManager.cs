using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public enum GameState {
        Start = 0, 
        Menu = 1,
        Lobby = 2,
        Game = 3,
        Win = 4,
        Loose = 5

    }

    void Start() => ChangeState(GameState.Start);

    public void ChangeState(GameState newState)
    {
        switch (newState) 
        { 
            case GameState.Start:
                break;
            case GameState.Menu:
                break;
            case GameState.Lobby:
                break;
            case GameState.Game:
                break;
            case GameState.Win:
                break;
            case GameState.Loose:
                break;
            default:
                break;
        }

    }
}
