using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{

    protected GameStateManager _gameStateManager;
    // было ли это состояние уже хоть раз установлено
    protected bool _wasSet;

    public void Init(GameStateManager gameStateManager) {
        _gameStateManager = gameStateManager;
    }

    protected abstract void EnterFirstTime();

    public virtual void Enter(){
        if (_wasSet == false) {
            _wasSet = true;
            EnterFirstTime();
        }
    }

    public abstract void Exit();

}
