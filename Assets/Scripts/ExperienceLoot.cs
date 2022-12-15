using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExperienceLoot : Loot
{

    // TODO: Разобраться че тут происходит
    public static event Action<Collector> OnCollectAll = delegate { };

    private void Awake()
    {
        LootType = LootType.Experience;
    }

    // TODO: Разобраться че тут происходит
    public static void TakeAll(Collector coinCollector) {
        OnCollectAll(coinCollector);
    }

    private void OnEnable()
    {
        OnCollectAll += Collect;
    }

    private void OnDisable()
    {
        OnCollectAll -= Collect;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        coinCollector.CollectExperienceLoot();
    }

}
