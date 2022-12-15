using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHealthEffect {
    public void OnSetDamage(ref float damage);
}

[CreateAssetMenu(fileName = nameof(HolyShieldEffect), menuName = "Effects/" + nameof(HolyShieldEffect))]
public class HolyShieldEffect : ContinuousEffect, IPlayerHealthEffect
{

    [SerializeField] private HolyShield _holyShieldPrefab;
    private HolyShield _holyShield;

    private bool _isActive;

    public void OnSetDamage(ref float damage)
    {
        if (_isActive) {
            damage = 0;
        }
    }

    protected override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        _holyShield = Instantiate(_holyShieldPrefab, _player.transform.position, Quaternion.identity, _player.transform);
        _holyShield.Deactivate();
        _player.PlayerHealth.AddHealthEffect(this);
    }

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine( LifeCycle(GetSkillValue(Skill.LifeTime) ) );
    }

    private IEnumerator LifeCycle(float lifeTime)
    {
        _isActive = true;
        _holyShield.Activate();
        yield return new WaitForSeconds(lifeTime);
        _isActive = false;
        _holyShield.Deactivate();
    }


}
