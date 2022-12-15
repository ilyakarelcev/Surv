using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChainLightning : MonoBehaviour
{

    private Enemy _targetEnemy;
    private float _speed;
    private float _damage;
    private int _jumpCount;
    [SerializeField] private LayerMask _layerMask;

    public void Init(Enemy targetEnemy, float damage, float speed, int jumpCount)
    {
        _damage = damage;
        _targetEnemy = targetEnemy;
        _speed = speed;
        _jumpCount = jumpCount;
        //Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            Vector3 toEnemy = _targetEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(toEnemy, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _speed * Time.deltaTime);
            if (transform.position == _targetEnemy.transform.position)
            {
                AffectEnemy();
                //Debug.Log(_jumpCount);
                _jumpCount--;
                if (_jumpCount > 0)
                {
                    GetNextEnemy();
                }
                else {
                    Die();
                }
            }
        }
        else
        {
            Die();
        }
    }

    void GetNextEnemy() {
        //Debug.Log("GetNextEnemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, _layerMask, QueryTriggerInteraction.Ignore);
        float minDistance = Mathf.Infinity;
        Collider nearestCollider = null;
        Collider curentCollider = _targetEnemy.GetComponent<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == curentCollider) continue;
            float distance = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCollider = colliders[i];
            }
        }
        if (nearestCollider)
        {
            _targetEnemy = nearestCollider.GetComponent<Enemy>();
        }
        else
        {
            Die();
        }
    }

    void AffectEnemy()
    {
        _targetEnemy.SetDamage(_damage, true);
    }

    void Die() {
        Destroy(gameObject);
    }

}
