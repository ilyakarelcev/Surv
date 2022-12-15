using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : Loot
{

    private void Awake()
    {
        LootType = LootType.Other;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        // TODO: ������ FindObjectOfType
        FindObjectOfType<PlayerHealth>().SetMaxHealth();
    }

}
