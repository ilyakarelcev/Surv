using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    private GameState _currentGameState;

    [SerializeField] private GameState _menuState;
    [SerializeField] private GameState _actionState;
    [SerializeField] private GameState _pauseState;
    [SerializeField] private GameState _winState;
    [SerializeField] private GameState _loseState;

    public void Init() {
        _menuState.Init(this);
        _actionState.Init(this);
        _pauseState.Init(this);
        _winState.Init(this);
        _loseState.Init(this);

        SetGameState(_menuState);
    }

    private void SetGameState(GameState gameState) {
        Debug.Log(gameState.name);
        if (_currentGameState) {
            _currentGameState.Exit();
        }
        gameState.Enter();
        _currentGameState = gameState;
    }

    public void SetMenu() {
        SetGameState(_menuState);
    }

    public void SetAction() {
        SetGameState(_actionState);
    }

    public void SetPause()
    {
        SetGameState(_pauseState);
    }

    public void SetWin()
    {
        SetGameState(_winState);
    }

    public void SetLose() {
        SetGameState(_loseState);
    }


}
