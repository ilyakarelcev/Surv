using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ChainLightningEffect), menuName = "Effects/" + nameof(ChainLightningEffect))]
public class ChainLightningEffect : ContinuousEffect
{

    [SerializeField] private ChainLightning _chainLightningPrefab;
    [SerializeField] private float _bulletSpeed;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        //Debug.Log(number + "  " + nearestEnemies.Length);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 position = _player.transform.position;
                ChainLightning chainLightning = Instantiate(_chainLightningPrefab, position, Quaternion.identity);
                chainLightning.Init(nearestEnemies[i], GetSkillValue(Skill.Damage), _bulletSpeed, (int)GetSkillValue(Skill.PassCount));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

}
