using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAura : MonoBehaviour
{

    private Transform _target;

    public void Init(Transform target) {
        _target = target;
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
    }

    public void SetRadius(float radius) { 
        transform.localScale = Vector3.one * radius * 2f;
    }

}
