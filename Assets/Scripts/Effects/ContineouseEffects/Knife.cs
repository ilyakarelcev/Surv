using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    [SerializeField] private Vector3 _rotationSpeed;
    [SerializeField] private Transform _model;
    [SerializeField] private Rigidbody _rigidbody;

    private float _damage;

    void Update()
    {
        _model.Rotate(_rotationSpeed * Time.deltaTime);
    }

    public void Init(Vector3 velocity, float damage)
    {
        _damage = damage;
        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Enemy>().SetDamage(_damage, true);
        Destroy(gameObject);
    }

}
