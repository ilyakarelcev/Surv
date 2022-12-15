using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ColldownEffect), menuName = "Effects/OneTime/" + nameof(ColldownEffect))]
public class ColldownEffect : OneTimeEffect
{

    [SerializeField] private float _percent = 0.04f; // -4%
    public override void Activate()
    {
        base.Activate();
        _player.ColldownReduction += _percent;
    }

}
