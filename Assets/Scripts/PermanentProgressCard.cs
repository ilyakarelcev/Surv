using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermanentProgressCard : MonoBehaviour
{

    public int PercentPerLevel;
    public int PricePerLevel;

    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Button _button;

    [SerializeField] private GameObject _priceBlock;
    [SerializeField] private GameObject _noMoneyObject;

    private Progress _progress;
    private Action<int> _addLevelAction;

    private bool _enoughMoney;

    public void Init(Progress progress, Action<int> action, int level) {
        _progress = progress;
        _addLevelAction = action;
        _button.onClick.AddListener(OnClick);
        Display(level);
    }

    public void SetLevel(int level) {
        Display(level);
    }

    int nextLevePrice;
    void Display(int level) {
        int percent = (level + 1) * PercentPerLevel;
        nextLevePrice = (level + 1) * PricePerLevel;
        _percentText.text = "+" + percent.ToString() + "%";
        _priceText.text = nextLevePrice.ToString();

        if (_progress.ProgressData.Coins >= nextLevePrice)
        {
            _priceBlock.SetActive(true);
            _noMoneyObject.SetActive(false);
            _enoughMoney = true;
        }
        else {
            _priceBlock.SetActive(false);
            _noMoneyObject.SetActive(true);
            _enoughMoney = false;
        }
    }


    void OnClick() {
        Debug.Log("OnClick");
        if (_enoughMoney)
        {
            AddLevel();
        }
        else { 
            //
        }
    }

    private void AddLevel() {
        _addLevelAction.Invoke(nextLevePrice);
    }

}
