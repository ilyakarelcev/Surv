using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AxesEffect), menuName = "Effects/" + nameof(AxesEffect))]
public class AxesEffect : ContinuousEffect
{

    [SerializeField] private Axes _axesPrefab;

    public override void Activate()
    {
        base.Activate();
        if (Level == 1) {
            Axes axes = Instantiate(_axesPrefab);
            axes.Setup(_effectsManager.transform);
        }
    }

}
