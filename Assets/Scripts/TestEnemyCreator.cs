using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyCreator : MonoBehaviour
{

    [SerializeField] private int _number;
    [SerializeField] private Transform _zone;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Enemy _enemyPrefab;

    void Start()
    {
        for (int i = 0; i < _number; i++)
        {
            Enemy newEnemy = _enemyManager.CreteEnemy(_enemyPrefab);
            newEnemy.transform.position = GetRandomZonePint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_zone.transform.position, _zone.transform.localScale);
    }

    Vector3 GetRandomZonePint() {
        return _zone.TransformPoint(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
    }

}
