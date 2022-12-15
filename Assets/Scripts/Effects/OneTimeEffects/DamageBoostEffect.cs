using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DamageBoostEffect), menuName = "Effects/OneTime/" + nameof(DamageBoostEffect))]
public class DamageBoostEffect : OneTimeEffect
{

    [SerializeField] private float _percent = 0.1f; // +10%
    public override void Activate()
    {
        base.Activate();
        _player.DamageBoost += _percent;
    }

}
