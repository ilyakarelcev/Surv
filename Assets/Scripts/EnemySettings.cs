using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{

    public AnimationCurve ScaleCurve;
    public AnimationCurve RotationCurve;
    public float ShakeTime = 0.5f;

}
