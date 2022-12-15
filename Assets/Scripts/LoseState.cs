using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{

    [SerializeField] private LoseWindow _window;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private GameManager _gameManager;

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0;
        _window.Show(_coinCounter, _gameManager);
    }

    public override void Exit()
    {
        // Никогда не выходим из этого состояния
    }

    protected override void EnterFirstTime()
    {
    }

}
