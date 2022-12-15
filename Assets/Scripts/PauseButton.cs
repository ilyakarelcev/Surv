using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{

    private GameStateManager _gameStateManager;
    [SerializeField] private GameState _gameState;
    private Button _button;

    public void Init(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _gameStateManager.SetPause();
    }

}
