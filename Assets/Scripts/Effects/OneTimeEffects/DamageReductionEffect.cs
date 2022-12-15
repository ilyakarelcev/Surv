using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DamageReductionEffect), menuName = "Effects/OneTime/" + nameof(DamageReductionEffect))]
public class DamageReductionEffect : OneTimeEffect
{

    [SerializeField] private float _percent = 0.05f; // +5%
    public override void Activate()
    {
        base.Activate();
        _player.DamageReduction += _percent;
    }

}
