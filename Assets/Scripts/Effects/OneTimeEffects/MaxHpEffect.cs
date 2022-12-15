using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MaxHpEffect), menuName = "Effects/OneTime/" + nameof(MaxHpEffect))]
public class MaxHpEffect : OneTimeEffect
{
    [SerializeField] private float _percent = 0.1f;
    public override void Activate()
    {
        base.Activate();
        _player.MaxHpBoost += _percent;
        _player.PlayerHealth.BoostMaxHp(_percent);
    }
}
