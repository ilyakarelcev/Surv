using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentProgress : MonoBehaviour
{

    private Progress _progress;

    [SerializeField] private PermanentProgressCard _healthCard;
    [SerializeField] private PermanentProgressCard _damageCard;
    [SerializeField] private PermanentProgressCard _lootCard;

    private CoinCounter _coinCounter;

    public void Init(Progress progress, CoinCounter coinCounter) {
        _progress = progress;
        _coinCounter = coinCounter;
        _healthCard.Init(progress, AddHealth, progress.ProgressData.HealthLevel);
        _damageCard.Init(progress, AddDamage, progress.ProgressData.DamageLevel);
        _lootCard.Init(progress, AddLoot, progress.ProgressData.LootLevel);
    }

    public void AddHealth(int price)
    {
        _progress.ProgressData.HealthLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
    }

    public void AddDamage(int price)
    {
        _progress.ProgressData.DamageLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
    }

    public void AddLoot(int price)
    {
        _progress.ProgressData.LootLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
    }

    private void UpdateCards() {
        _healthCard.SetLevel(_progress.ProgressData.HealthLevel);
        _damageCard.SetLevel(_progress.ProgressData.DamageLevel);
        _lootCard.SetLevel(_progress.ProgressData.LootLevel);
    }

    public float GetHealth() { 
        return _healthCard.PercentPerLevel * _progress.ProgressData.HealthLevel * 0.01f;
    }

    public float GetDamage()
    {
        return _damageCard.PercentPerLevel * _progress.ProgressData.DamageLevel * 0.01f;
    }

    public float GetLoot() { 
        return _lootCard.PercentPerLevel * _progress.ProgressData.LootLevel * 0.01f;
    }

}
