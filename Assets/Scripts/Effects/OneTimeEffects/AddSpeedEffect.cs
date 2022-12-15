using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddSpeedEffect), menuName = "Effects/OneTime/" + nameof(AddSpeedEffect))]
public class AddSpeedEffect : OneTimeEffect
{

    [SerializeField] private float _percent;
    public override void Activate()
    {
        base.Activate();
        _player.MovementSpeed += _percent;
    }

}
