using UnityEngine;


public class MagicMissles : MonoBehaviour
{

    private Enemy _targetEnemy;
    private float _speed;
    private float _damage;

    public void Init(Enemy targetEnemy, float damage, float speed) {
        _damage = damage;
        _targetEnemy = targetEnemy;
        _speed = speed;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _speed * Time.deltaTime);
            if (transform.position == _targetEnemy.transform.position)
            {
                AffectEnemy();
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    void AffectEnemy() {
        _targetEnemy.SetDamage(_damage, true);
    }

}