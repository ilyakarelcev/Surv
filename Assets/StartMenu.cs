using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private Button _tapToStartButton;
    private GameStateManager _gameStateManager;

    public void Init(GameStateManager gameStateManager) {
        _gameStateManager = gameStateManager;
        _tapToStartButton.onClick.AddListener(_gameStateManager.SetAction);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
