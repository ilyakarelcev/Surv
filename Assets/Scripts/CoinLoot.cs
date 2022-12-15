using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : Loot
{

    [SerializeField] private int _value = 5;

    private void Awake()
    {
        LootType = LootType.Coin;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        coinCollector.CollectCoin(_value);
    }

}
