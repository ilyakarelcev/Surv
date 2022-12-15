using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    private float _damage;
    private float _radius;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private LayerMask _layerMask;

    public void Init(float damage, float radius)
    {
        _radius = radius;
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask, QueryTriggerInteraction.Ignore);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy)) {
                enemy.SetDamage(_damage, true);
            }
        }
        Destroy(gameObject);
    }

}
