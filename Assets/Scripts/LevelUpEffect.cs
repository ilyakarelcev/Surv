using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{

    [SerializeField] private float _lifeTime;
    [SerializeField] protected ParticleSystem _particleSystem;
    private Coroutine _coroutine;

    public void StartEffect(Transform target) {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(LifeCycle(target));
    }

    private IEnumerator LifeCycle(Transform target) {
        _particleSystem.Play();
        float birthTime = Time.time;
        while (Time.time - birthTime < _lifeTime)
        {
            transform.position = target.position;
            yield return null;
        }
        _coroutine = null;
    }

}
