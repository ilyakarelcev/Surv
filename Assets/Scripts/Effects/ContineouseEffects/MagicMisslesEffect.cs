using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MagicMisslesEffect), menuName = "Effects/" + nameof(MagicMisslesEffect))]
public class MagicMisslesEffect : ContinuousEffect
{

    [SerializeField] private MagicMissles _magicMisslesPrefab;
    [SerializeField] private float _bulletSpeed;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess() {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 position = _player.transform.position;
                MagicMissles magicMissles = Instantiate(_magicMisslesPrefab, position, Quaternion.identity);
                magicMissles.Init(nearestEnemies[i], GetSkillValue(Skill.Damage), _bulletSpeed);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }


}
