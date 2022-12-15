using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyHit : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _text;
    
    private float _valueBefore;
    private float _value;

    private float _lastShowTime;

    private Coroutine _coroutine;

    public void Init() {
        gameObject.SetActive(false);
    }

    public void ShowDamage(Vector3 position, float damage) {
        transform.position = position;
        _lastShowTime = Time.time;
        _value += damage;
        if (_coroutine == null) {
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(Show());
        }
    }

    private IEnumerator Show() {
        
        while (Time.time - _lastShowTime < 1f)
        {
            if (_valueBefore != _value) {
                _valueBefore = _value;
                _text.text = "-" + _value.ToString("#");
            }
            yield return null;
        }
        _value = 0f;
        _valueBefore = 0f;
        gameObject.SetActive(false);
        _coroutine = null;
    }

}
