using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RockShootingEffect), menuName = "Effects/" + nameof(RockShootingEffect))]
public class RockShootingEffect : ContinuousEffect
{

    [SerializeField] private RockShooting _rockShooting;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 playerPosition = _player.transform.position;
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0f,360f), 0);
                RockShooting newRockShooting = Instantiate(_rockShooting, playerPosition, rotation);

                newRockShooting.Init(GetSkillValue(Skill.Damage), GetSkillValue(Skill.Radius));
                //newFireBall.Init(direction.normalized * _speed, GetFeatureValue(Feature.Radius), GetFeatureValue(Feature.Damage));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }


}
