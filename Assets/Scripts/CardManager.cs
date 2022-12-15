using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardManagerParent;
    [SerializeField] private Card[] _effectCards;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private TextMeshProUGUI _levelText;


    private void Awake()
    {
        for (int i = 0; i < _effectCards.Length; i++)
        {
            _effectCards[i].Init(this, _effectsManager);
        }
    }

    public void ShowCards(List<Effect> effects, int level) {
        _cardManagerParent.SetActive(true);

        _levelText.text = level.ToString();

        for (int i = 0; i < effects.Count; i++)
        {
            _effectCards[i].Show(effects[i]);
        }
        // Если нужно показать меньше чем 3 эффекта
        for (int i = effects.Count; i < 3; i++)
        {
            _effectCards[i].Hide();
        }
    }

    public void HideCards() {
        _cardManagerParent.SetActive(false);
    }

}
