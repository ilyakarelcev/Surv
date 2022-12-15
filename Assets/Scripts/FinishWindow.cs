using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Button _multiplyCoinsButton;
    [SerializeField] private Button _continueButton;
    private CoinCounter _coinCounter;
    private GameManager _gameManager;

    public void Show(CoinCounter coinCounter, GameManager gameManager)
    {
        gameObject.SetActive(true);
        _coinCounter = coinCounter;
        _gameManager = gameManager;
        SetCoinsText();
        _multiplyCoinsButton.onClick.AddListener(MultiplyCoins);
        _continueButton.onClick.AddListener(Continue);
    }

    public void Hide()
    {
    }

    private void MultiplyCoins()
    {
        // Тут надо вызвать показ рекламы

        _multiplyCoinsButton.gameObject.SetActive(false);

        // Умножаем количество монет, заработанных на уровне на 3
        int coinsToAdd = Mathf.RoundToInt(_coinCounter.NumberInLevel) * 2;
        _coinCounter.AddCoins(coinsToAdd);
        SetCoinsText();
    }

    private void SetCoinsText() {
        _coinsText.text = Mathf.RoundToInt(_coinCounter.NumberInLevel).ToString();
    }

    private void Continue()
    {
        _gameManager.Restart();
    }
}
