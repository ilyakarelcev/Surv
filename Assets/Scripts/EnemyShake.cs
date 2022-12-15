using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShake : MonoBehaviour
{

    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemySettings _enemySettings;

    private Vector3 _startScale;

    private void Awake()
    {
        _enemy.OnTakeDamdage += Shake;
        _startScale = transform.localScale;
    }

    private Coroutine _shakeProcess;
    public void Shake()
    {
        if (_shakeProcess == null)
        {
            _shakeProcess = StartCoroutine(ShakeProcess());
        }
    }

    private IEnumerator ShakeProcess()
    {
        Vector3 targetScale = Vector3.Scale(_startScale, new Vector3(1.2f, 0.8f, 1.2f));
        for (float t = 0; t < 1f; t += Time.deltaTime / _enemySettings.ShakeTime)
        {
            float scale = _enemySettings.ScaleCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(_startScale, targetScale, scale);
            float xEuler = _enemySettings.RotationCurve.Evaluate(t);
            transform.localRotation = Quaternion.Euler(xEuler, 0f, 0f);
            yield return null;
        }
        transform.localScale = _startScale;
        transform.localRotation = Quaternion.identity;

        _shakeProcess= null;
    }

}
