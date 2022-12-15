using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private float _damage;
    private float _radius;

    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _layerMask;

    public void Init(Vector3 velocity, float radius, float damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        _radius = radius;
        Destroy(gameObject, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Enemy>().SetDamage(_damage, true);
        }
        Destroy(gameObject);
    }

}
