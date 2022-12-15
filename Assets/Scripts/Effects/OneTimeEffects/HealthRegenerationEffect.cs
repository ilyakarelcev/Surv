using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(HealthRegenerationEffect), menuName = "Effects/OneTime/" + nameof(HealthRegenerationEffect))]
public class HealthRegenerationEffect : OneTimeEffect
{

    [SerializeField] private float _percent = 0.01f;
    public override void Activate()
    {
        base.Activate();
        _player.HealthRegeneration += _percent;
    }

}
