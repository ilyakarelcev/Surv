using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Фон эффекта, может быть синий или оранжевый
    [SerializeField] private Image _iconBackground;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _button;

    [SerializeField] private Sprite _continuousEffectSprite;
    [SerializeField] private Sprite _oneTimeEffectSprite;

    private CardManager _cardManager;
    private EffectsManager _effectsManager;

    private Effect _effect;

    public void Init(CardManager cardManager, EffectsManager effectsManager) {
        _button.onClick.AddListener(OnClick);
        _cardManager = cardManager;
        _effectsManager = effectsManager;
    }

    public void Show(Effect effect) {

        if (effect is ContinuousEffect continuousEffect) {
            _iconBackground.sprite = _continuousEffectSprite;
            _descriptionText.text = GetDescription(continuousEffect, effect.Level);
        }
        if (effect is OneTimeEffect)
        {
            _descriptionText.text = effect.Description;
            _iconBackground.sprite = _oneTimeEffectSprite;
        }

        _effect = effect;
        _iconImage.sprite = effect.Sprite;
        _nameText.text = effect.Name;

        //
        _levelText.text = "LVL " + (effect.Level + 1);
        gameObject.SetActive(true);
    }

    private string GetDescription(ContinuousEffect effect, int level) {

        // Если у эффекта нулевой уровень, то показывается просто его описание из его ScriptableObject-а
        if (level == 0) return effect.Description;

        // 
        string result = "";
        for (int i = 0; i < ContinuousEffect.GetTotalNumberOfSkills(); i++)
        {
            if (effect.ActiveSkills[i] == true)
            {
                Skill skill = (Skill)i;
                float thisLevelValue = effect.GetSkillValue(skill, false);
                float nextLevelValue = effect.GetSkillValue(skill, true);
                float delta = nextLevelValue - thisLevelValue;
                if (delta == 0) continue;

                result += ContinuousEffect.GetSkillName(skill);
                result += ": ";

                result += "<color=#ffc000>";
                result += thisLevelValue.ToString("0.00");
                result += "</color>";

                result += "<color=#68e900>";
                result += " ";
                if (delta > 0)
                {
                    result += "+";
                }
                result += delta.ToString("0.00");
                result += "</color>";

                result += "\n";
            }
        }
        return result;
    }


    public void OnClick() {
        _effectsManager.ClickCard(_effect);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
