using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BlackHoleEffect), menuName = "Effects/" + nameof(BlackHoleEffect))]
public class BlackHoleEffect : ContinuousEffect
{

    [SerializeField] private BlackHole _blackHolePrefab;
    
    protected override void Produce()
    {
        base.Produce();
        BlackHole newBlackHole = Instantiate(_blackHolePrefab, _player.transform.position, Quaternion.identity);
        newBlackHole.Init(GetSkillValue(Skill.LifeTime), GetSkillValue(Skill.Radius), GetSkillValue(Skill.DPS));
    }

}
