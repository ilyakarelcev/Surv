using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(NovaEffect), menuName = "Effects/" + nameof(NovaEffect))]
public class NovaEffect : ContinuousEffect
{

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Nova _novaPrefab;
    private Nova _currentNova;
    
    protected override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        _currentNova = Instantiate(_novaPrefab, _player.transform);
        _currentNova.transform.localPosition = Vector3.zero;
    }

    protected override void SetLevel()
    {
        base.SetLevel();
        _currentNova.SetRadius(GetSkillValue(Skill.Radius));
    }

    protected override void Produce()
    {
        base.Produce();
        _currentNova.ShowEffect();
        Collider[] colliders = Physics.OverlapSphere(_currentNova.transform.position, GetSkillValue(Skill.Radius), _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Enemy>() is Enemy enemy)
            {
                float damage = GetSkillValue(Skill.Damage);
                enemy.SetDamage(damage,true);
            }
        }
    }


}
