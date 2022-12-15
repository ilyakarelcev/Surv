using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProjectileCountEffect), menuName = "Effects/OneTime/" + nameof(ProjectileCountEffect))]
public class ProjectileCountEffect : OneTimeEffect
{

    public override void Activate()
    {
        base.Activate();
        _player.ProjectileCount++;
    }

}
