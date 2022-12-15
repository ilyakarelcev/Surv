using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseState : GameState
{

    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameManager _gameManager;
    
    protected override void EnterFirstTime()
    {
        //base.EnterFirstTime();
        _continueButton.onClick.AddListener(Continue);
        _restartButton.onClick.AddListener(Restart);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _pauseWindow.Show();
        _pauseButton.SetActive(false);
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
        _pauseWindow.Hide();
        _pauseButton.SetActive(true);
    }

    private void Continue() {
        _gameStateManager.SetAction();
    }

    private void Restart()
    {
        // Перезагрузка уровня. В игре всего один уровень
        _gameManager.Restart();
    }

}
