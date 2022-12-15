using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Nova : MonoBehaviour
{

    [SerializeField] private float _radius;
    [SerializeField] ParticleSystem _particleSystem;

    public void SetRadius(float value) {
        transform.localScale = Vector3.one * value * 2f;
    }

    public void ShowEffect()
    {
        _particleSystem.Play();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
    }
#endif


}
