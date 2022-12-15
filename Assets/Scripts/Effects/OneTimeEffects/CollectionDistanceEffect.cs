using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CollectionDistanceEffect), menuName = "Effects/OneTime/" + nameof(CollectionDistanceEffect))]
public class CollectionDistanceEffect : OneTimeEffect
{

    [SerializeField] private float _distanceToAdd = 0.5f;

    public override void Activate()
    {
        base.Activate();
        _player.CollectionDistanceBoost += _distanceToAdd;
    }

}
