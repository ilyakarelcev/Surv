using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeteorEffect), menuName = "Effects/" + nameof(MeteorEffect))]
public class MeteorEffect : ContinuousEffect
{

    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] private float _zoneRadius = 12f;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));

        for (int i = 0; i < number; i++)
        {
            Debug.Log("i = " + i);
            Vector2 randomPosition = Random.insideUnitCircle;
            Vector3 position = _player.transform.position + new Vector3(randomPosition.x, 0, randomPosition.y) * _zoneRadius;
            Meteor newMeteor = Instantiate(_meteorPrefab, position, Quaternion.identity);

            newMeteor.Init(GetSkillValue(Skill.Radius), GetSkillValue(Skill.Damage));

            yield return new WaitForSeconds(0.3f);
        }
                
            
        
    }

}
