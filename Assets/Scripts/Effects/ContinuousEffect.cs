using System;
using UnityEngine;
using UnityEngine.Events;

public enum Skill
{
    Colldown,
    Damage,
    Radius,
    Number, //ProjectilesCount
    DPS,
    PassCount, //PassThroughCount
    LifeTime
}

[System.Serializable]
public class SkillLevels {
    public float[] Values = new float[10];
}

public class ContinuousEffect : Effect
{

    [HideInInspector]
    public bool[] ActiveSkills = new bool[GetTotalNumberOfSkills()];
    [HideInInspector]
    public SkillLevels[] SkillLevels = new SkillLevels[GetTotalNumberOfSkills()];
    [HideInInspector]
    public SkillLevels[] SkillLevelsAdditions = new SkillLevels[GetTotalNumberOfSkills()];

    [HideInInspector]
    public UnityEvent<float> OnLoad = new UnityEvent<float>();
    private float _timer;


    public static int GetTotalNumberOfSkills()
    {
        return Enum.GetValues(typeof(Skill)).Length;
    }

    public float GetSkillValue(Skill skill, bool nextLevel = false)
    {
        int levelIndex = Level - 1 + (nextLevel ? 1 : 0);

        float value = SkillLevels[(int)skill].Values[levelIndex];

        if (skill == Skill.Colldown) {
            value *= (1 - _player.ColldownReduction);
        } else if (skill == Skill.Damage) {
            value *= (1 + _player.DamageBoost);
        }
        else if (skill == Skill.Radius)
        {
            value *= (1 + _player.RadiusBoost);
        }
        else if (skill == Skill.Number)
        {
            value += _player.ProjectileCount;
        }
        else if (skill == Skill.DPS)
        {
            value += _player.DamageBoost;
        }
        return value;
    }

    public void ProcessFrame(float frameTime)
    {
        _timer += frameTime;
        float colldown = GetSkillValue(Skill.Colldown);

        OnLoad.Invoke(_timer / colldown);

        if (_timer > colldown)
        {
            _timer = 0;
            Produce();
        }
    }

    protected virtual void Produce()
    {
    }

    public static string GetSkillName(Skill skill)
    {
        if (skill == Skill.Colldown)
            return "Colldown";
        else if (skill == Skill.Damage)
            return "Damage";
        else if (skill == Skill.Radius)
            return "Radius";
        else if (skill == Skill.Number)
            return "Number";
        else if (skill == Skill.DPS)
            return "DPS";
        else if (skill == Skill.PassCount)
            return "Pass Count";
        else if (skill == Skill.LifeTime)
            return "Life Time";
        else return "Нет названия для такого эффекта";
    }

}
