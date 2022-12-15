using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpellRadiusEffect), menuName = "Effects/OneTime/" + nameof(SpellRadiusEffect))]
public class SpellRadiusEffect : OneTimeEffect
{

    [SerializeField] private float _percent = 0.05f; // 5%

    public override void Activate()
    {
        base.Activate();
        _player.RadiusBoost += _percent;
    }

}
