using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RockShooting : MonoBehaviour
{

    private float _damage;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _damageSphere;
    [SerializeField] private float _flyTime = 0.2f;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private TriggerWithEvent _triggerWithEvent;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Init(10f, 8f);
        }
    }

    public void Init(float damage, float size)
    {
        _damage = damage;
        StartCoroutine(LifeCycle());
        _triggerWithEvent.OnTrigger += EnterEnemy;
        _particleSystem.Play();
        transform.localScale = Vector3.one * size;
    }

    public void EnterEnemy(Enemy enemy)
    {
        enemy.SetDamage(_damage, true);
    }

    private IEnumerator LifeCycle()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _flyTime)
        {
            //float zPosition = t * _target.localPosition.z;
            //_damageSphere.localPosition = new Vector3(0, 0, zPosition);
            _damageSphere.localScale = new Vector3(1, 1, t);
            yield return new WaitForFixedUpdate();
        }
        //_damageSphere.localPosition = new Vector3(0, 0, _target.localPosition.z);
        _damageSphere.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1.7f);
        //Destroy(_damageSphere.gameObject);
        Destroy(gameObject);
    }


}








//Collider[] colliders = Physics.OverlapSphere(_damageSphere.transform.position, _radius, _layerMask, QueryTriggerInteraction.Ignore);
//for (int i = 0; i < colliders.Length; i++) {
//    colliders[i].GetComponent<Enemy>().SetDamage(_damage);
//}