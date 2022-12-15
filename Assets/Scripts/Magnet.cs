using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Loot
{

    private void Awake()
    {
        LootType = LootType.Other;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        // TODO: беза с ответственностью
        ExperienceLoot.TakeAll(coinCollector);
    }

}
