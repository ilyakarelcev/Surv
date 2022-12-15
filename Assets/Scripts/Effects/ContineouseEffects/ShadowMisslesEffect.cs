using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShadowMisslesEffect), menuName = "Effects/" + nameof(ShadowMisslesEffect))]
public class ShadowMisslesEffect : ContinuousEffect
{

    [Space(8)]
    [SerializeField] private ShadowMissile _shadowMisslePrefab;
    [SerializeField] private float _bulletSpeed;

    protected override void Produce()
    {
        base.Produce();
        Transform playerTransform = _player.transform;
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        //Debug.Log("number " + number);
        for (int i = 0; i < number; i++)
        {
            float angle = (360f / number) * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * playerTransform.forward;
            ShadowMissile newBullet = Instantiate(_shadowMisslePrefab, playerTransform.position, Quaternion.identity);
            int passCount = Mathf.RoundToInt(GetSkillValue(Skill.PassCount));
            newBullet.Setup(direction * _bulletSpeed, GetSkillValue(Skill.Damage), passCount);
        }
    }


}
