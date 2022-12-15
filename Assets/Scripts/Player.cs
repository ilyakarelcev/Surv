using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerHealth PlayerHealth;
    private Progress _progress;
    private PermanentProgress _permanentProgress;

    public void Init(PermanentProgress permanentProgress)
    {
        _permanentProgress = permanentProgress;
    }

    private float _damageBoost; // +10% ��
    public float HealthRegeneration; // +1% ��
    private float _maxHpBoost; //+10% ��
    public float DamageReduction; // +5% ��
    public float MovementSpeed; // +10%   ��
    public float CollectionDistanceBoost; // +50% ��
    public int ProjectileCount; // +1 ��
    public float ColldownReduction; // +4% ��
    public float RadiusBoost; //+5% ��

    public float DamageBoost
    {
        get
        {
            return _damageBoost * (1 + _permanentProgress.GetDamage());
        }
        set => _damageBoost = value;
    }

    public float MaxHpBoost
    {
        get
        {
            return _maxHpBoost * (1 + _permanentProgress.GetHealth());
        }
        set => _maxHpBoost = value;
    }



}
