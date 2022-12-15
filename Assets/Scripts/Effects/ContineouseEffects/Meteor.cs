using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    [SerializeField] private float _delayBeforeDamage = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _delayBeforeDie = 2f;


    private float _radius;
    private float _damage;

    public void Init(float radius, float damage) {
        _radius = radius;
        _damage = damage;
        transform.localScale = Vector3.one * _radius;
        StartCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle() { 
        yield return new WaitForSeconds(_delayBeforeDamage);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Enemy>().SetDamage(_damage, true);
        }
        yield return new WaitForSeconds(_delayBeforeDie);
        Destroy(gameObject);
    }



}
