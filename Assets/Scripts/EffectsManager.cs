using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    //public RigidbodyMove RigidbodyMove;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private List<ContinuousEffect> _continuousEffectsApplied = new List<ContinuousEffect>();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffectsApplied = new List<OneTimeEffect>();

    //[SerializeField] private List<Effect> _effects = new List<Effect>();
    [SerializeField] private List<ContinuousEffect> _continuousEffects = new List<ContinuousEffect>();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffects = new List<OneTimeEffect>();

    [SerializeField] private CardManager _cardManager;
    [SerializeField] private Player _player;

    [SerializeField] private TopIconManager _topIconManager;
    public Action OnHideCards;

    private void Awake()
    {
        // Заполняем массивы копиями, чтоб не изменять оригиналы
        for (int i = 0; i < _continuousEffects.Count; i++)
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
            _continuousEffects[i].Initialize(this, _enemyManager, _player);
        }
        for (int i = 0; i < _oneTimeEffects.Count; i++)
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
            _oneTimeEffects[i].Initialize(this, _enemyManager, _player);
        }
    }



    [ContextMenu(nameof(ShowCards))]
    public void ShowCards(int level)
    {
        Debug.Log("ShowCards");
        bool onlyContinuous = level == 1;

        List<Effect> effectsToShow = new List<Effect>();
        for (int i = 0; i < _continuousEffectsApplied.Count; i++)
        {
            if (_continuousEffectsApplied[i].Level < 10)
            {
                effectsToShow.Add(_continuousEffectsApplied[i]);
            }
        }

        for (int i = 0; i < _oneTimeEffectsApplied.Count; i++)
        {
            if (_oneTimeEffectsApplied[i].Level < 10)
            {
                effectsToShow.Add(_oneTimeEffectsApplied[i]);
            }
        }
        if (_continuousEffectsApplied.Count < 4)
        {
            effectsToShow.AddRange(_continuousEffects);
        }

        if (_oneTimeEffectsApplied.Count < 4 && onlyContinuous == false)
        {
            effectsToShow.AddRange(_oneTimeEffects);
        }

        int numverOfCardsToShow = Mathf.Min(effectsToShow.Count, 3);
        int[] randomIndexes = RandomSort(effectsToShow.Count, numverOfCardsToShow);

        List<Effect> effectsForCards = new List<Effect>();
        for (int i = 0; i < randomIndexes.Length; i++)
        {
            int index = randomIndexes[i];
            effectsForCards.Add(effectsToShow[index]);
        }

        _cardManager.ShowCards(effectsForCards, level); //
        
    }

    void HideCards()
    {
        _cardManager.HideCards();
        OnHideCards.Invoke();
    }

    public int[] RandomSort(int length, int number)
    {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }
        for (int i = 0; i < array.Length; i++)
        {
            int oldValue = array[i];
            int newIndex = UnityEngine.Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }
        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = array[i];
        }
        return result;
    }

    public void ClickCard(Effect effect)
    {
        if (effect is ContinuousEffect c_effect)
        {
            if (!_continuousEffectsApplied.Contains(c_effect))
            {
                _continuousEffectsApplied.Add(c_effect);
                _continuousEffects.Remove(c_effect);
                _topIconManager.AddIccon(c_effect);
            }

        }
        else if (effect is OneTimeEffect o_effect)
        {
            if (!_oneTimeEffectsApplied.Contains(o_effect))
            {
                _oneTimeEffectsApplied.Add(o_effect);
                _oneTimeEffects.Remove(o_effect);
                _topIconManager.AddIccon(o_effect);
            }
        }
        effect.Activate();

        HideCards();
    }

    void Update()
    {
        foreach (var effect in _continuousEffectsApplied)
        {
            effect.ProcessFrame(Time.deltaTime * (1 + _player.ColldownReduction) );
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowCards(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowCards(2);
        }
    }
}
