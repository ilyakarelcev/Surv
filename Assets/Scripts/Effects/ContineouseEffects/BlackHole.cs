using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    private Vector3 _targetDirection;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rotationLerpRate = 2f;
    [SerializeField] private float _changeDirectionInterval = 1f;
    private float _timer;

    private const int _arraySize = 50;
    private Collider[] _colliders = new Collider[_arraySize];
    private Enemy[] _enemiesToDrag = new Enemy[_arraySize];

    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _layerMask;

    private float _dps;
    //[SerializeField] private float _lifeTime;

    public void Init(float lifeTime, float radius, float dps) { 
        _radius = radius;
        transform.localScale = Vector3.one * _radius * 2f;
        _dps = dps;
        StartCoroutine(DieProcess(lifeTime));
    }

    private void Start()
    {
        SetRandomTargetDirection();
        StartCoroutine(GetEnemiesToDrag());
    }

    private IEnumerator GetEnemiesToDrag()
    {
        while (true)
        {
            int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);

            for (int i = 0; i < numberOfColliders; i++)
            {
                _enemiesToDrag[i] = _colliders[i].GetComponent<Enemy>();
            }
            for (int i = numberOfColliders; i < _arraySize; i++)
            {
                _enemiesToDrag[i] = null;
            }

            yield return new WaitForSeconds(0.16f);
        }
    }

    private void FixedUpdate()
    {
        if (_enemiesToDrag == null) return;

        for (int i = 0; i < _enemiesToDrag.Length; i++)
        {
            if (_enemiesToDrag[i] == null) break;
            Vector3 direction = transform.position - _enemiesToDrag[i].transform.position;
            Vector3 velocity = direction.normalized * _dragSpeed;
            _enemiesToDrag[i].Drag(velocity, Time.deltaTime);
            _enemiesToDrag[i].SetDamage(_dps * Time.deltaTime);
        }
    }

    private Vector3 _currentDirection;
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _changeDirectionInterval)
        {
            _timer = 0;
            SetRandomTargetDirection();
        }

        
        _currentDirection = Vector3.Slerp(_currentDirection, _targetDirection, Time.deltaTime * _rotationLerpRate);
        //Quaternion targetRotation = Quaternion.LookRotation(_targetDirection, Vector3.up);
        //Quaternion rotation = Quaternion.LookRotation(_currentDirection, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerpRate);
        Debug.DrawRay(transform.position, _targetDirection, Color.blue);
        Debug.DrawRay(transform.position, _currentDirection, Color.red);

        transform.position += _currentDirection * _speed * Time.deltaTime;
    }

    private void SetRandomTargetDirection()
    {
        Vector2 randomDirection = (Random.insideUnitCircle).normalized;
        _targetDirection = new Vector3(randomDirection.x, 0, randomDirection.y);
    }

    private IEnumerator DieProcess(float lifeTime) {
        yield return new WaitForSeconds(lifeTime);
        float startScale = transform.localScale.x;
        for (float t = 0; t < 1f; t+=Time.deltaTime)
        {
            float scale = startScale * (1 - t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
    }
#endif

}
