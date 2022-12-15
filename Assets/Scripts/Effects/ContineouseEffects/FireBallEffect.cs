using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FireBallEffect), menuName = "Effects/" + nameof(FireBallEffect))]
public class FireBallEffect : ContinuousEffect
{

    [SerializeField] private FireBall _fireBallPrefab;
    [SerializeField] private float _speed;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number)) + _player.ProjectileCount;
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 playerPosition = _player.transform.position;
                FireBall newFireBall = Instantiate(_fireBallPrefab, playerPosition, Quaternion.identity);

                Vector3 direction = Quaternion.Euler(0, Random.Range(0f, 360f), 0) * Vector3.right;

                newFireBall.Init(direction.normalized * _speed, GetSkillValue(Skill.Radius), GetSkillValue(Skill.Damage));
                yield return new WaitForSeconds(0.2f);

            }
        }
    }

}
