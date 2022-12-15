using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FrostAuraEffect), menuName = "Effects/" + nameof(FrostAuraEffect))]
public class FrostAuraEffect : ContinuousEffect
{

    [SerializeField] private FrostAura _frostAuraPrefab;
    [SerializeField] private FrostAura _frostAura;

    private Collider[] _colliders = new Collider[20];
    [SerializeField] private LayerMask _layerMask;

    private const float _hitPeriod = 0.1f;

    protected override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        _frostAura = Instantiate(_frostAuraPrefab, _player.transform.position, Quaternion.identity);
        _frostAura.Init(_player.transform);
        _effectsManager.StartCoroutine(HitEnemies());
    }

    protected override void SetLevel()
    {
        base.SetLevel();
        _frostAura.SetRadius(GetSkillValue(Skill.Radius));
    }

    private IEnumerator HitEnemies()
    {
        while (true)
        {
            float radius = GetSkillValue(Skill.Radius);
            int numberOfColliders = Physics.OverlapSphereNonAlloc(_player.transform.position, radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < numberOfColliders; i++)
            {
                _colliders[i].GetComponent<Enemy>().SetDamage( GetSkillValue(Skill.DPS) * _hitPeriod );
            }
            yield return new WaitForSeconds(_hitPeriod);
        }
    }


}
