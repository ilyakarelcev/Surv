using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ThrowingKnivesEffect), menuName = "Effects/" + nameof(ThrowingKnivesEffect))]
public class ThrowingKnivesEffect : ContinuousEffect
{

    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private float _speed;

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
            // Позиция ножа с небольшим разбросом
            Vector3 position = _player.transform.position + _player.transform.right * Random.Range(-0.3f, 0.3f);
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward);
            Knife newKnife = Instantiate(_knifePrefab, position, rotation);
            newKnife.Init(_player.transform.forward * _speed, GetSkillValue(Skill.Damage));
            yield return new WaitForSeconds(0.2f);
        }

    }

}
