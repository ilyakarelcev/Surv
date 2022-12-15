using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{

    [SerializeField] private Joystick _joystick;
    [SerializeField] private RigidbodyMove _rigidbodyMove;
    [SerializeField] private GameManager _gameManager;

    protected override void EnterFirstTime()
    {
        //base.EnterFirstTime();
        _gameManager.Init(_gameStateManager);
        _gameManager.SetFirstLevel();
    }

    public override void Enter()
    {
        base.Enter();
        _joystick.Activate();
        _rigidbodyMove.enabled = true;
    }

    public override void Exit()
    {
        _joystick.Deactivate();
        _rigidbodyMove.enabled = false;
    }

}
